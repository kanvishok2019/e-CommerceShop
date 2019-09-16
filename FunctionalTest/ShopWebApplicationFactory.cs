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

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    try
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<ShopDbContext>();
                        db.Database.EnsureCreated();

                        ShopDbContextSeed.SeedAsync(db).Wait();
                    }
                    catch (Exception ex)
                    {
                        //Log Exception
                    }
                }

            });


        }
    }
}
