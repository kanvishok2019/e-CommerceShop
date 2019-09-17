using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Command;
using Infrastructure.Core.Query;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Models;
using ShoppingCart.ApplicationCore.Basket.Commands;
using ShoppingCart.ApplicationCore.Basket.Query;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMapper _autoMapper;

        public TestDataController(ICommandBus commandBus, IMapper autoMapper, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _autoMapper = autoMapper;
            _queryBus = queryBus;
        }

        [HttpPost]
        public async Task Post()
        {

            await CreatePurchaseOrder(1, new[] { 1 });
            await CreatePurchaseOrder(2, new[] { 2 });
            await CreatePurchaseOrder(3, new[] { 1, 2 });
            await CreatePurchaseOrder(4, new[] { 1, 2, 3, 4 });
            await CreatePurchaseOrder(5, new[] { 2, 3, 4 });
            await CreatePurchaseOrder(6, new[] { 1, 3, 4 });
            await CreatePurchaseOrder(7, new[] { 4 });
        }

        private async Task CreatePurchaseOrder(int buyerId, int[] catalogItemIds)
        {
            var basketModel = new CreateBasketModel { BuyerId = buyerId };
            var createNewBasketCommand = _autoMapper.Map<CreateBasketModel, CreateBasketForUserCommand>(basketModel);

            await _commandBus.SendAsync(createNewBasketCommand);

            var basket = await _queryBus.SendAsync<GetBasketByBuyerId, Basket>(new GetBasketByBuyerId { BuyerId = buyerId });

            foreach (var catalogItemId in catalogItemIds)
            {
                var basketItemModel = new BasketItemModel
                { BasketId = basket.BasketId, Price = 10M, Quantity = 1, CatalogItemId = catalogItemId };
                var addItemToBasketCommand = _autoMapper.Map<BasketItemModel, AddItemToBasketCommand>(basketItemModel);
                await _commandBus.SendAsync(addItemToBasketCommand);
            }

            basket = await _queryBus.SendAsync<GetBasketByBuyerId, Basket>(new GetBasketByBuyerId { BuyerId = buyerId });

            var createPurchaseOrderModel = new CreatePurchaseOrderModel();
            createPurchaseOrderModel.BasketId = basket.BasketId.ToString();
            var shippingAddress = new Address("126 BowhillWay", "Harlow",
                "Essex", "United Kingdom", "CM20 2FH");
            var crateOrderCommand = new CreatePurchaseOrderCommand(Guid.Parse(createPurchaseOrderModel.BasketId), shippingAddress);
            await _commandBus.SendAsync(crateOrderCommand);

        }
    }
}