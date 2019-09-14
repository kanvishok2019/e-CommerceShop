using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core.Domain
{
    public interface IEntity
    {
        object Key { get; }
    }
}
