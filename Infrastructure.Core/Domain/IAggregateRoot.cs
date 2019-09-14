using System.Text;

namespace Infrastructure.Core.Domain
{
    public interface IAggregateRoot: IEventSourced, IEntity
    {
        bool Active { get; }
    }
}
