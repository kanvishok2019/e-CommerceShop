using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Query
{
    public class QueryBus : IQueryBus
    {
        private readonly IDictionary<Type, IQueryHandler> _queryHandlers;

        public QueryBus()
        {
            _queryHandlers = new Dictionary<Type, IQueryHandler>();
        }

        public async Task SubscribeAsync<TQuery, TResult>(IQueryHandler<TQuery, TResult> queryHandler) where TQuery : class, IQuery<TResult>
        {
            await Task.Run(() =>
            {
                if (queryHandler == null)
                {
                    throw new ArgumentNullException(nameof(queryHandler));
                }

                var existingQueryHandler = GetQueryHandler<TQuery>();
                if (existingQueryHandler != null)
                {
                    throw new InvalidOperationException(
                        $"Command handler already subscribed for the command {typeof(TQuery)}");
                }

                _queryHandlers.Add(typeof(TQuery), queryHandler);
            });
        }

        private IQueryHandler GetQueryHandler<TQuery>()
        {
            IQueryHandler existingQueryHandlers;
            _queryHandlers.TryGetValue(typeof(TQuery), out existingQueryHandlers);
            return existingQueryHandlers;
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>
        {
            var commandHandler = GetQueryHandler<TQuery>() as IQueryHandler<TQuery, TResult>;

            if (commandHandler == null)
                return default(TResult);

            return await commandHandler.HandleAsync(query);
        }
    }
}
