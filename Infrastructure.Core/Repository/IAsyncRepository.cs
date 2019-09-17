using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Core.Repository
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetSingleAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task AddAllAsync(List<T> entities);
        T Add(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
