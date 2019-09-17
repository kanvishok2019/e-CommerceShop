using Infrastructure.Core.EventStore;
using Infrastructure.Core.Repository;

namespace Infrastructure.Data.Repository
{
    public sealed class EventStoreSpecification : BaseSpecification<EventStore>
    {
        public EventStoreSpecification(string aggregateId, string type) :
            base(b => b.AggregateId == aggregateId && b.AggregateType == type)
        {
             ApplyOrderBy(x => x.Version);
        }
    }
}
