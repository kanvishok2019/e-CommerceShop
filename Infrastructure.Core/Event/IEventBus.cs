using System.Threading.Tasks;

namespace Infrastructure.Core.Event
{
    public interface IEventBus
    {
        Task SubscribeAsync<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent;
        Task SendAsync<TEvent>(TEvent payload) where TEvent : class, IEvent;
    }
}
