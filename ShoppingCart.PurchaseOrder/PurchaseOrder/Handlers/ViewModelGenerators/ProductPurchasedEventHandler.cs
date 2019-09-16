using System;
using System.Threading.Tasks;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Events;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.ViewModelGenerators
{
    public class ShippingInvoiceViewModelGenerator:IEventHandler<ProductPurchasedEvent>
    {
        public Task HandleAsync(ProductPurchasedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
