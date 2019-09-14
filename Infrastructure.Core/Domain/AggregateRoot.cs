using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core.Domain
{
    public abstract class AggregateRoot : EventSourced, IAggregateRoot
    {
        public AggregateRoot(object entityKey) :base(entityKey)
        {
            Active = true;
            AggregateRoot = this;
        }

        public bool Active { get; }
    }
   
}
