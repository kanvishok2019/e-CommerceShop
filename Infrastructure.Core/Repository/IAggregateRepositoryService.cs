using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Domain;

namespace Infrastructure.Core.Repository
{
    public interface IAggregateRepositoryService<T> where T : class, IAggregateRoot
    {
        Task<T> GetAsync<TKey>(TKey aggregateRootKey)
            where TKey : struct;
        Task Remove<TKey>(TKey aggregateRootKey)
            where TKey : struct;
        Task SaveAsync(T aggregate);
    }
}
