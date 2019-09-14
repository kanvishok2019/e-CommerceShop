using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Event;

namespace Infrastructure.Core.EventStore
{
    public interface IEventSourceRepositoryService
    {
        Task SaveAllEvents(IList<IVersionedEvent> domainEvents, ITextSerializer textSerializer);
        Task SaveEvent(IVersionedEvent versionedEvent, ITextSerializer textSerializer);
    }
}
