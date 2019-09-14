using System;

namespace Infrastructure.Core.Command
{
    public interface ICommand
    {
        void Initialize(Guid correlationId = default(Guid));
    }
}