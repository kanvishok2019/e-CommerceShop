using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Core.Event;

namespace Infrastructure.Core.Domain
{
    public abstract class EventSourced:IEventSourced
    {
        protected abstract void RegisterUpdateHandlers();
        private readonly Dictionary<Type, Action<IVersionedEvent>> _handlers = new Dictionary<Type, Action<IVersionedEvent>>();
        public uint Version { get; protected set; }
        public Queue<IVersionedEvent> UnCommittedEvents { get; }
        public object Key { get; }
        public IAggregateRoot AggregateRoot { get; protected set; }
        private bool _isUpdateHandlersRegistered;

        protected EventSourced(object entityKey)
        {
            if (!entityKey.IsSimpleType())
            {
                throw new ArgumentException("Key cannot be a complex type");
            }

            Key = entityKey;
            UnCommittedEvents = new Queue<IVersionedEvent>();
        }

        public void RegisterUpdateHandler<TEvent>(Action<TEvent> eventHandler) where TEvent : class, IEvent
        {
            _handlers.Add(typeof(TEvent), (versionedEvent) => eventHandler((TEvent)versionedEvent));
        }

        public virtual void AddEvent(IVersionedEvent versionedEvent)
        {
            if (!AggregateRoot.Active)
            {
                throw new InvalidOperationException("No changes can be made to this aggregate, since it is not active");
            }

            if (!versionedEvent.IsInitialized)
            {
                versionedEvent.Initialize(Key.ToString(), GetType().Name, ++Version,
                    AggregateRoot.Key.ToString(), AggregateRoot.GetType().Name);
            }

            ApplyUpdate(versionedEvent);
            UnCommittedEvents.Enqueue(versionedEvent);
        }

        public void ApplyUpdate(IEnumerable<IVersionedEvent> versionedEventsHistory)
        {

            foreach (var versionedEvent in versionedEventsHistory)
            {
                ApplyUpdate(versionedEvent);
                Version = versionedEvent.Version;
            }
        }

        public virtual void ApplyUpdate(IVersionedEvent versionedEvent)
        {
            if (!_isUpdateHandlersRegistered)
            {
                RegisterUpdateHandlers();
                _isUpdateHandlersRegistered = true;
            }

            if (_handlers.TryGetValue(versionedEvent.GetType(), out var eventHandler))
            {
                eventHandler(versionedEvent);
            }

            var eventSourcedEntities = GetChildEntities();
            if (!eventSourcedEntities.Any())
            {
                return;
            }

            eventSourcedEntities.ToList().ForEach(eventSourcedEntity =>
            {
                eventSourcedEntity.ApplyUpdate(versionedEvent);
            });
        }

        public virtual IList<IEventSourced> GetChildEntities()
        {
            return new List<IEventSourced>();
        }
    }
}
