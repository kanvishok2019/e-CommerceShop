﻿using System;
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
    public class AggregateRepositoryService<T> : IAggregateRepositoryService<T> where T : class, IAggregateRoot
    {
        private readonly IEventBus _eventBus;
        private readonly ITextSerializer _textSerializer;
        private readonly IAsyncRepository<EventStore> _eventStoreRepositoryService;
        private readonly IUnitOfWork _unitOfWork;

        public AggregateRepositoryService(ITextSerializer textSerializer, IEventBus eventBus, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventStoreRepositoryService = _unitOfWork.GetRepositoryAsync<EventStore>();
            _textSerializer = textSerializer;
            _eventBus = eventBus;

        }

        public async Task<T> GetAsync<TKey>(TKey aggregateRootKey) where TKey : struct
        {
           return await Task.Run(() =>
              {
                  var eventStoreSpecification = new EventStoreSpecification(aggregateRootKey.ToString(), typeof(T).Name);
                  var eventHistoryList = new List<IVersionedEvent>();
                  foreach (var eventHistory in _eventStoreRepositoryService.ListAsync(eventStoreSpecification).Result)
                  {
                      eventHistoryList.Add(Deserialize(eventHistory));
                  }

                  if (eventHistoryList.Any())
                  {
                      var aggregateConstructFactory =
                          typeof(T).GetConstructor(new[] { typeof(TKey), typeof(IEnumerable<IVersionedEvent>) });
                      if (aggregateConstructFactory != null)
                      {
                          return (T)aggregateConstructFactory.Invoke(new object[] { aggregateRootKey, eventHistoryList });
                      }
                  }

                  return null;

              });
        }
        private IVersionedEvent Deserialize(EventStore versionedEvent)
        {
            return (IVersionedEvent)_textSerializer.Deserialize(versionedEvent.Payload);
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
            await _unitOfWork.SaveChangesAsync();


        }

        public async Task SaveEvent(IVersionedEvent versionedEvent, ITextSerializer textSerializer)
        {
            if (versionedEvent == null)
            {
                throw new ArgumentNullException(nameof(versionedEvent));
            }

            await SaveAllEvents(new[] { versionedEvent }, textSerializer);
        }

        public async Task PublishAllUncommittedEvents(Queue<IVersionedEvent> uncommittedEvents)
        {
            if (uncommittedEvents == null || uncommittedEvents.Count == 0)
                return;
            do
            {
                var versionedEvent = uncommittedEvents.Dequeue();
                await _eventBus.SendAsync(versionedEvent);
            } while (uncommittedEvents.Count != 0);
        }

        private EventStore GetSerializedEvent(IVersionedEvent versionedEvent, ITextSerializer textSerializer)
        {
            var serializedEvent = new EventStore
            {
                CorrelationId = versionedEvent.CorrelationId,
                Version = (int)versionedEvent.Version,
                AggregateType = versionedEvent.AggregateRootType,
                AggregateId = versionedEvent.AggregateRootId,
                Payload = textSerializer.Serialize(versionedEvent),
                OccurredDateTime = DateTime.UtcNow
            };

            return serializedEvent;
        }
    }
}
