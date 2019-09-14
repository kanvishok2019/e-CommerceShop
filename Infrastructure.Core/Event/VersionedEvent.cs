using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core.Event
{
    public class VersionedEvent : Event, IVersionedEvent
    {
        public string EntityId { get; private set; }
        public string EntityType { get; private set; }

        public uint Version { get; private set; }
        public string AggregateRootId { get; private set; }
        public string AggregateRootType { get; private set; }

        public bool IsInitialized { get; private set; }

        public void Initialize(string sourceId, string sourceType, uint version, string sourceRootId, string sourceRootType, Guid correlationId = default(Guid))
        {
            if (!String.IsNullOrWhiteSpace(EntityId) ||
                !String.IsNullOrWhiteSpace(EntityType) ||
                Version != 0 ||
                !String.IsNullOrWhiteSpace(AggregateRootId) ||
                !String.IsNullOrWhiteSpace(AggregateRootType))
            {
                throw new ArgumentException("Event is immutable. Once the value assigned cannot be changed");
            }

            base.Initialize(correlationId);

            if (String.IsNullOrWhiteSpace(sourceId))
            {
                throw new ArgumentNullException(nameof(sourceId));
            }

            if (String.IsNullOrWhiteSpace(sourceType))
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (version == 0)
            {
                throw new ArgumentNullException(nameof(version), @"version cannot be zero");
            }

            if (version == 0)
            {
                throw new ArgumentNullException(nameof(version), @"version cannot be zero");
            }

            EntityId = sourceId;
            EntityType = sourceType;
            Version = version;
            AggregateRootId = sourceRootId;
            AggregateRootType = sourceRootType;
            IsInitialized = true;
        }
    }
}
