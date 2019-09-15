using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Infrastructure.Core.EventStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EventStoreDbContext: DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());
        }

        public DbSet<EventStore> EventStores { get; set; }
    }
}
