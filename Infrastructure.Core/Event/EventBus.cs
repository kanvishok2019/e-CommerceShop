using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Event
{
    public class EventBus : IEventBus
    {
        private readonly IDictionary<Type, IList<IEventHandler>> _eventHandlers;

        public EventBus()
        {
            _eventHandlers = new Dictionary<Type, IList<IEventHandler>>();
        }

        public Task SubscribeAsync<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            return Task.Run(() =>
            {
                if (eventHandler == null)
                {
                    throw new ArgumentNullException(nameof(eventHandler));
                }

                var existingEventHandlers = GetEventHandlers(typeof(TEvent));
                existingEventHandlers.Add(eventHandler);
            });
        }

        private IList<IEventHandler> GetEventHandlers(Type eventType)
        {
            IList<IEventHandler> existingEventHandlers;
            if (!_eventHandlers.TryGetValue(eventType, out existingEventHandlers))
            {
                existingEventHandlers = new List<IEventHandler>();
                _eventHandlers.Add(eventType, existingEventHandlers);
            }

            return existingEventHandlers;
        }

        public async Task SendAsync<TEvent>(TEvent payload) where TEvent : class, IEvent
        {
            var eventHandlers = GetEventHandlers(payload.GetType());

            foreach (var eventHandler in eventHandlers)
            {
                var eventHandlerInvoker = BuildEventHandlerInvoker(payload.GetType());
                await eventHandlerInvoker.Invoke(eventHandler, payload);
            }
        }

        private Func<IEventHandler, IEvent, Task> BuildEventHandlerInvoker(Type payloadType)
        {
            var eventParameter = Expression.Parameter(typeof(IEvent));
            var eventHandlerParameter = Expression.Parameter(typeof(IEventHandler));
            var eventHandlerGenericType = typeof(IEventHandler<>).MakeGenericType(payloadType);

            var eventHandleInvokerExression = Expression.Lambda(
                Expression.Block(
                    Expression.Call(Expression.Constant(this),
                        this.GetType().GetMethod("DispatchEvent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).MakeGenericMethod(payloadType),
                        Expression.Convert(eventHandlerParameter, eventHandlerGenericType),
                        Expression.Convert(eventParameter, payloadType)
                    )),
                eventHandlerParameter,
                eventParameter
            );

            return (Func<IEventHandler, IEvent, Task>)eventHandleInvokerExression.Compile();
        }

        private async Task DispatchEvent<TEvent>(IEventHandler<TEvent> eventHandler, TEvent payload) where TEvent : class, IEvent
        {
            await eventHandler.HandleAsync(payload);
        }
    }
}
