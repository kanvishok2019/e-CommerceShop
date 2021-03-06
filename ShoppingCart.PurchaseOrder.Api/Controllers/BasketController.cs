﻿using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Command;
using Infrastructure.Core.Query;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Models;
using ShoppingCart.ApplicationCore.Basket.Commands;
using ShoppingCart.ApplicationCore.Basket.Query;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMapper _autoMapper;

        public BasketController(ICommandBus commandBus, IMapper autoMapper, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _autoMapper = autoMapper;
            _queryBus = queryBus;
        } 

        [HttpGet("{buyerId}")]
        public async Task<Basket> Get(int buyerId)
        {
           var basket = await _queryBus.SendAsync<GetBasketByBuyerId, Basket>(new GetBasketByBuyerId {BuyerId = buyerId});
            return basket;
        }

         [HttpPost]
        public async Task Post([FromBody] CreateBasketModel createBasketModel)
        {
            var createNewBasketCommand = _autoMapper.Map<CreateBasketModel, CreateBasketForUserCommand>(createBasketModel);
            await _commandBus.SendAsync(createNewBasketCommand);
        }
    
        [HttpPut("{basketId}")]
        public async Task Put([FromBody] BasketItemModel basketItemModel)
        {
            //Assumption: We will call the Product Api to get the product
            //pricing and other details, So that user cannot tamper the price
            var addItemToBasketCommand = _autoMapper.Map<BasketItemModel, AddItemToBasketCommand>(basketItemModel);

            await _commandBus.SendAsync(addItemToBasketCommand);
        }
       
    }
}
