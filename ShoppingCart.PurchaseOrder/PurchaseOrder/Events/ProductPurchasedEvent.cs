using System.Collections.Generic;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class ProductPurchasedEvent : VersionedEvent
    {
        public readonly IEnumerable<PurchaseOrderItem> PurchaseOrderList;
        public int PurchaseOrderNo { get;  }
        public int BuyerId { get; }

        public ProductPurchasedEvent(int purchasePurchaseOrderNo, int buyerId, IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            PurchaseOrderNo = purchasePurchaseOrderNo;
            PurchaseOrderList = purchaseOrderItem;
            BuyerId = buyerId;
        }
    }
}