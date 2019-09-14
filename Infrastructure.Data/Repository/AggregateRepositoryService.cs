using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Core;
using Infrastructure.Core.Domain;
using Infrastructure.Core.Event;
using Infrastructure.Core.EventStore;
using Infrastructure.Core.Repository;

namespace Infrastructure.Data.Repository
{
    public class AggregateRepositoryService<T> : IAggregateRepositoryService<T>, IEventSourceRepositoryService where T : class, IAggregateRoot
    {
        private readonly IEventBus _eventBus;
        private readonly IEventStoreRepositoryService _eventStoreRepositoryService;
        private readonly ITextSerializer _textSerializer;

        public AggregateRepositoryService(IEventBus eventBus, IEventStoreRepositoryService eventStoreRepositoryService, ITextSerializer textSerializer)
        {
            _eventBus = eventBus;
            _textSerializer = textSerializer;
            _eventStoreRepositoryService = eventStoreRepositoryService;
        }

        public Task<T> GetAsync<TKey>(TKey aggregateRootKey) where TKey : struct
        {
            throw new System.NotImplementedException();
        }

        public Task Remove<TKey>(TKey aggregateRootKey) where TKey : struct
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveAsync(T aggregate)
        {
            var evenSourcedAggregate = aggregate as IEventSourced;
            if (evenSourcedAggregate == null)
                return;
            await SaveAllEvents(evenSourcedAggregate.UnCommittedEvents.ToList(), _textSerializer);
            await PublishAllUncommittedEvents(evenSourcedAggregate.UnCommittedEvents);
        }
        public async Task SaveAllEvents(IList<IVersionedEvent> uncommittedEvents, ITextSerializer textSerializer)
        {
            if (uncommittedEvents == null)
            {
                throw new ArgumentNullException(nameof(uncommittedEvents));
            }

            if (textSerializer == null)
            {
                throw new ArgumentNullException(nameof(textSerializer));
            }

            var serializedEvents =
                uncommittedEvents.Select(@event => GetSerializedEvent(@event, textSerializer)).ToList();

            await _eventStoreRepositoryService.AddAllAsync(serializedEvents);
            await _eventStoreRepositoryService.SaveChangesAsync();
        }

        public async Task SaveEvent(IVersionedEvent versionedEvent, ITextSerializer textSerializer)
        {
            if (versionedEvent == null)
            {
                throw new ArgumentNullException(nameof(versionedEvent));
            }

            await SaveAllEvents(new[] { versionedEvent }, textSerializer);
        }

        private async Task PublishAllUncommittedEvents(Queue<IVersionedEvent> uncommittedEvents)
        {
            if (uncommittedEvents == null || uncommittedEvents.Count == 0)
            {
                return;
            }

            do
            {
                var versionedEvent = uncommittedEvents.Dequeue();
                await _eventBus.SendAsync(versionedEvent);
            } while (uncommittedEvents.Count != 0);
        }
        private EventStore GetSerializedEvent(IVersionedEvent versionedEvent, ITextSerializer textserializer)
        {
            var serializedEvent = new EventStore
            {
                CorrelationId = versionedEvent.CorrelationId,
                Version = (int)versionedEvent.Version,
                AggregateType = versionedEvent.AggregateRootType,
                AggregateId = versionedEvent.AggregateRootId,
                Payload = textserializer.Serialize(versionedEvent),
                OccurredDateTime = DateTime.UtcNow
            };

            return serializedEvent;
        }
    }
}
