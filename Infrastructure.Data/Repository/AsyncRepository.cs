using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Core.Domain;
using Infrastructure.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;

        public AsyncRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).SingleAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task AddAllAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _dbContext.Set<T>().Remove(entity); });
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
