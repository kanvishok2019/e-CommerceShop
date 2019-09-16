using System.Collections.Generic;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class ProductPurchasedEvent : VersionedEvent
    {
        private readonly IEnumerable<PurchaseOrderItem> _purchaseOrderList;
        public ProductPurchasedEvent(IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            _purchaseOrderList = purchaseOrderItem;
        }
    }
}