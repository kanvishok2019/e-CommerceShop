using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Command;
using Infrastructure.Core.Query;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Models;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/purchase-orders")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMapper _autoMapper;

        public PurchaseOrderController(ICommandBus commandBus, IMapper autoMapper, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _autoMapper = autoMapper;
            _queryBus = queryBus;
        }

     
        [HttpPost]
        public async Task Post([FromBody] CreatePurchaseOrderModel createPurchaseOrderModel)
        {
            //Assumption: We call an API Or we Get Address From User

            var shippingAddress = new Address("126 BowhillWay","Harlow",
                "Essex","United Kingdom","CM20 2FH");
            var crateOrderCommand = new CreatePurchaseOrderCommand(Guid.Parse(createPurchaseOrderModel.BasketId), shippingAddress);
            await _commandBus.SendAsync(crateOrderCommand);
        }

        [HttpPost("{purchaseOrderNo}/process")]
        public async Task Post(int purchaseOrderNo)
        {
            var processPurchaseOrderCommand = new ProcessPurchaseOrderCommand(purchaseOrderNo);
            await _commandBus.SendAsync(processPurchaseOrderCommand);
        }

    }
}