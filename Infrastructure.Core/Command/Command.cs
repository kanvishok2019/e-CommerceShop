using System;
using System.Collections.Generic;
using System.Text;


namespace Infrastructure.Core.Command
{
    public class Command : ICommand
    {
        public Guid Id { get; }

        public DateTime CreatedAt { get; }
        public Guid CorrelationId { get; private set; }

        public Command()
        {
            Id = new Guid();
            CreatedAt = DateTime.UtcNow;
        }
        public void Initialize(Guid correlationId = default(Guid))
        {
            if (CorrelationId != default(Guid))
            {
                throw new ArgumentException("Command is immutable. Once the value assigned, cannot be changed");
            }

            CorrelationId = correlationId;
        }
    }
}
