using System.Threading.Tasks;

namespace Infrastructure.Core.Query
{
    public interface IQueryHandler
    {

    }

    public interface IQueryHandler<in TQuery, TResult> : IQueryHandler
        where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
