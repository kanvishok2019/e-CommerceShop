using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core.Event
{
    public interface IVersionedEvent
    {
        string EntityId { get; }
        string EntityType { get; }
        uint Version { get; }
        string AggregateRootId { get; }
        string AggregateRootType { get; }
        bool IsInitialized { get; }
        void Initialize(string sourceId, string sourceType, uint version, string sourceRootId, string sourceRootType, Guid correlationId = default(Guid));
    }
}
