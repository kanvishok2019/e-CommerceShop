using System;

namespace Infrastructure.Core.Event
{
    public class Event : IEvent
    {
        public Event()
        {
            //Id = Guid.NewGuid();
            Id = new Guid();
            CreatedAt = DateTime.UtcNow;
        }

        public void Initialize(Guid correlationId = default(Guid))
        {
            if (CorrelationId != default(Guid))
            {
                throw new ArgumentException("Event is immutable. Once the value assigned cannot be changed");
            }
            CorrelationId = correlationId;
        }

        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Guid CorrelationId { get; private set; }
    }
}
