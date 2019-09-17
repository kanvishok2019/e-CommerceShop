using System.Threading.Tasks;

namespace Infrastructure.Core.Query
{
    public interface IQueryBus
    {
        Task SubscribeAsync<TQuery, TResult>(IQueryHandler<TQuery, TResult> queryHandler) where TQuery : class, IQuery<TResult>;
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
    }
}
