using System;
using System.Collections.Generic;
using Infrastructure.Core.Event;

namespace Infrastructure.Core.Domain
{
    public interface IEventSourced
    {
        Queue<IVersionedEvent> UnCommittedEvents { get; }

        void RegisterUpdateHandler<TEvent>(Action<TEvent> eventHandler)
            where TEvent : class, IEvent;

        void AddEvent(IVersionedEvent versionedEvent);

        void ApplyUpdate(IEnumerable<IVersionedEvent> versionedEventsHistory);

        void ApplyUpdate(IVersionedEvent versionedEvent);

        IList<IEventSourced> GetChildEntities();
    }
}