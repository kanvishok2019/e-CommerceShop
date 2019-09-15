using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Infrastructure.Core.EventStore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;

namespace Infrastructure.Data
{
   public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());
        }

        public DbSet<EventStore> EventStores { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
