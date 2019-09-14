using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Repository;

namespace Infrastructure.Core.EventStore
{
                     
    public interface IEventStoreRepositoryService : IAsyncRepository<EventStore>
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
