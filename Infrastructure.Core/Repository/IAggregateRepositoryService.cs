using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Core.Domain;
using Infrastructure.Core.Event;

namespace Infrastructure.Core.Repository
{
    public interface IAggregateRepositoryService<T> where T : class, IAggregateRoot
    {
        Task<T> GetAsync<TKey>(TKey aggregateRootKey)
            where TKey : struct;
        Task SaveAsync(T aggregate);
        Task SaveAllEvents(IList<IVersionedEvent> domainEvents, ITextSerializer textSerializer);
        Task SaveEvent(IVersionedEvent versionedEvent, ITextSerializer textSerializer);
        Task PublishAllUncommittedEvents(Queue<IVersionedEvent> unCommittedEvents);
    }
}
