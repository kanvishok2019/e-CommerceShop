using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Infrastructure.Core.Event;
using Infrastructure.Core.EventStore;
using Infrastructure.Core.Repository;

namespace Infrastructure.Data.Repository
{
    public sealed class EventStoreSpecification : BaseSpecification<EventStore>
    {
        public EventStoreSpecification(string aggregateId, string type) :
            base(b => b.AggregateId == aggregateId && b.AggregateType == type)
        {
            //ApplySelect(selectorFunc);
            ApplyOrderBy(x => x.Version);

        }
    }
}
