using System.Threading.Tasks;
using Infrastructure.Core.Domain;
using Infrastructure.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public UnitOfWork(TContext context)
        {
            Context = context;
        }
        public TContext Context { get; }
        public IAsyncRepository<T> GetRepositoryAsync<T>() where T : BaseEntity
        {
            return new AsyncRepository<T>(Context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
