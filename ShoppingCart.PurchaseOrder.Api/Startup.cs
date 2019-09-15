﻿using System;
using AutoMapper;
using Infrastructure.Core;
using Infrastructure.Core.Command;
using Infrastructure.Core.Event;
using Infrastructure.Core.EventStore;
using Infrastructure.Core.Query;
using Infrastructure.Core.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Api.Configurators;
using ShoppingCart.ApplicationCore.Basket.Commands;
using ShoppingCart.ApplicationCore.Basket.Domain;
using ShoppingCart.ApplicationCore.Basket.Events;
using ShoppingCart.ApplicationCore.Basket.Handlers.CommandHandlers;
using ShoppingCart.ApplicationCore.Basket.Handlers.QueryHandlers;
using ShoppingCart.ApplicationCore.Basket.Handlers.ViewModelGenerators;
using ShoppingCart.ApplicationCore.Basket.Query;

namespace ShoppingCart.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //Note: this will enable in memory database
            ConfigureInMemoryDatabases(services);

            // Note: This will enable real database
            //ConfigureProductionServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            services.AddDbContext<ShopDbContext>(c =>
                c.UseInMemoryDatabase("Shop")).AddUnitOfWork<ShopDbContext>();

            //services.AddDbContext<EventStoreDbContext>(c =>
            //    c.UseInMemoryDatabase("EventStore")).AddUnitOfWork<EventStoreDbContext>();

            ConfigureServices(services);
        }
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<ShopDbContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString("ShopContextConnection")))
                .AddUnitOfWork<ShopDbContext>();

            //services.AddDbContext<EventStoreDbContext>(c =>
            //        c.UseSqlServer(Configuration.GetConnectionString("EventStoreContextConnection")))
            //    .AddUnitOfWork<EventStoreDbContext>();

            ConfigureServices(services);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<ITextSerializer, JsonTextSerializer>();

            services.AddTransient<Func<ITextSerializer>>(container =>
                container.GetService<ITextSerializer>);
            services.AddTransient(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped(typeof(IAggregateRepositoryService<>), typeof(AggregateRepositoryService<>));
            
            //Command
            services.AddScoped<ICommandHandler<AddItemToBasketCommand>, AddItemToBasketHandler>();
            services.AddScoped<ICommandHandler<CreateBasketForUserCommand>, CreateBasketForUserCommandHandler>();
            services.AddScoped<ICommandBus>(container =>
            {
                var commandBus = new CommandBus();
                commandBus.SubscribeAsync(container.GetService<ICommandHandler<CreateBasketForUserCommand>>()).Wait();
                commandBus.SubscribeAsync(container.GetService<ICommandHandler<AddItemToBasketCommand>>()).Wait();
                return commandBus;
            });

            //Event
            services.AddScoped<IEventHandler<BasketCreatedEvent>, BasketViewModelGenerator>();
            services.AddScoped<IEventHandler<ItemAddedToBasketEvent>, BasketItemAddedViewModelGenerator>();
            services.AddScoped<IEventBus>(container =>
            {
                var eventBus = new EventBus();
                eventBus.SubscribeAsync(container.GetService<IEventHandler<BasketCreatedEvent>>()).Wait();
                eventBus.SubscribeAsync(container.GetService<IEventHandler<ItemAddedToBasketEvent>>()).Wait();
                return eventBus;
            });

            //Query
            services.AddScoped<IQueryHandler<GetBasketByBuyerId,
                ApplicationCore.Basket.Query.ViewModel.Basket>, BasketQueryHandlers>();
            services.AddScoped<BasketQueryHandlers>();
            services.AddScoped<IQueryBus>(container =>
            {
                var queryBus = new QueryBus();
                queryBus.SubscribeAsync(container.GetService<IQueryHandler<GetBasketByBuyerId, ApplicationCore.Basket.Query.ViewModel.Basket>>()).Wait();
                return queryBus;
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.AddProfile(new AutoMappingConfiguration());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
