using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Events;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.EventHandlers
{
    public class SubscriptionItemPurchasedEventHandler :IEventHandler<SubscriptionItemPurchasedEvent>
    {
        public Task HandleAsync(SubscriptionItemPurchasedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
