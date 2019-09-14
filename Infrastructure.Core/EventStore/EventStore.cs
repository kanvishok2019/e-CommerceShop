using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core.EventStore
{
    public class EventStore
    {
        public Int64 Id { get; set; }
        public string AggregateId { get; set; }
        public string AggregateType { get; set; }
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public string Payload { get; set; }
        public DateTimeOffset OccurredDateTime { get; set; }
    }
}
