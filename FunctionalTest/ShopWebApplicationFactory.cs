using System;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingCart.Api;

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
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ShopDbContext>();
                    db.Database.EnsureCreated();
                    var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();

                    var logger = scopedServices
                        .GetRequiredService<ILogger<ShopWebApplicationFactory<TStartup>>>();
                    try
                    {
                        ShopDbContextSeed.SeedAsync(db).Wait();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }

            });
        }
    }
}
