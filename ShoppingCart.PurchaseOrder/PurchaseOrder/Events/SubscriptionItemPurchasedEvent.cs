using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class SubscriptionItemPurchasedEvent: VersionedEvent
    {
        private readonly IEnumerable<PurchaseOrderItem> _purchaseOrderList;
        public SubscriptionItemPurchasedEvent(IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            _purchaseOrderList = purchaseOrderItem;
        }
    }
}
