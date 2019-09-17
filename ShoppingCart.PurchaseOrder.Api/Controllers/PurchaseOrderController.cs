using System;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using Infrastructure.Core.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Api.Models;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/purchase-orders")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(ICommandBus commandBus, IQueryBus queryBus, ILogger<PurchaseOrderController> logger)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _logger = logger;
        }


        [HttpPost]
        public async Task Post([FromBody] CreatePurchaseOrderModel createPurchaseOrderModel)
        {
            _logger.LogInformation($"Create purchase order requested for basket {createPurchaseOrderModel.BasketId}");

            //Assumption: We call an API Or we get Address From User
            var shippingAddress = new Address("126 BowhillWay", "Harlow",
                "Essex", "United Kingdom", "CM20 2FH");
            var crateOrderCommand = new CreatePurchaseOrderCommand(Guid.Parse(createPurchaseOrderModel.BasketId), shippingAddress);
            await _commandBus.SendAsync(crateOrderCommand);
        }

        [HttpPost("{purchaseOrderNo}/process")]
        public async Task<IActionResult> Post(int purchaseOrderNo)
        {
            if (purchaseOrderNo == 0)
            {
                _logger.LogError("Purchase order number is required to process the purchase order");
                return BadRequest();
            }
            _logger.LogInformation($"Purchase order process requested for purchase order no {purchaseOrderNo}");
            var processPurchaseOrderCommand = new ProcessPurchaseOrderCommand(purchaseOrderNo);
            await _commandBus.SendAsync(processPurchaseOrderCommand);

            var shippingInvoiceQuery = new ShippingInvoiceQuery(purchaseOrderNo);
            return Ok(_queryBus.SendAsync<ShippingInvoiceQuery, ShippingInvoice>(shippingInvoiceQuery));
        }

    }
}