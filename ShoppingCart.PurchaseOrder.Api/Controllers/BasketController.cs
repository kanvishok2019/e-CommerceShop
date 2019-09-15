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

namespace ShoppingCart.PurchaseOrder.Api.Controllers
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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //[HttpGet("get-basket-id/{username}", Name = "get-basket-id")]
        [HttpGet("{buyerId}")]
        public async Task<Basket> Get(string buyerId)
        {
           var basket = await _queryBus.SendAsync<GetBasketByBuyerId, Basket>(new GetBasketByBuyerId {BuyerId = buyerId});
            return basket;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] CreateBasketModel createBasketModel)
        {
            var createNewBasketCommand = _autoMapper.Map<CreateBasketModel, CreateBasketForUserCommand>(createBasketModel);
            await _commandBus.SendAsync(createNewBasketCommand);
        }

        // PUT api/values/5
        //[HttpPut("add-item-to-basket/{id}", Name = "add-item-to-basket")]
        [HttpPut("{id}")]
        public async Task Put([FromBody] BasketItemModel basketItemModel)
        {
            var addItemToBasketCommand = _autoMapper.Map<BasketItemModel, AddItemToBasketCommand>(basketItemModel);

            await _commandBus.SendAsync(addItemToBasketCommand);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
