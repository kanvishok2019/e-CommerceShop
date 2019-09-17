using System;
using System.Threading.Tasks;
using Infrastructure.Core.Domain;

namespace Infrastructure.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<T> GetRepositoryAsync<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
