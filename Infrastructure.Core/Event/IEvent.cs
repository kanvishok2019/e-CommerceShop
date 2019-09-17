using System;

namespace Infrastructure.Core.Event
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        Guid CorrelationId { get; }
        void Initialize(Guid correlationId);
    }
}
