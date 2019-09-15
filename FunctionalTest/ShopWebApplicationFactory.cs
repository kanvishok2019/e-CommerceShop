using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Command;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Api;
using ShoppingCart.ApplicationCore.Basket.Commands;
using ShoppingCart.PurchaseOrder.Api;

namespace FunctionalTest
{
    public class ShopWebApplicationFactory<TStartup>
        : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var provider = services
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ShopDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ShopDb");
                    options.UseInternalServiceProvider(provider);
                }).AddUnitOfWork<ShopDbContext>();

                services.AddDbContext<EventStoreDbContext>(options =>
                {
                    options.UseInMemoryDatabase("EventStoreDb");
                    options.UseInternalServiceProvider(provider);
                }).AddUnitOfWork<EventStoreDbContext>();
                var sp = services.BuildServiceProvider();

            });
         

        }
    }
}
