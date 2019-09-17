using System.Collections.Generic;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class SubscriptionItemPurchasedEvent : VersionedEvent
    {
        public readonly IEnumerable<PurchaseOrderItem> PurchaseOrderList;
        public int PurchaseOrderNo { get; }
        public int BuyerId { get; }
        public bool IsPurchaseOrderProcessed { get; }
        public SubscriptionItemPurchasedEvent(int purchasePurchaseOrderNo, int buyerId, IEnumerable<PurchaseOrderItem> purchaseOrderItem, bool isPurchaseOrderProcessed)
        {
            PurchaseOrderNo = purchasePurchaseOrderNo;
            BuyerId = buyerId;
            PurchaseOrderList = purchaseOrderItem;
            IsPurchaseOrderProcessed = isPurchaseOrderProcessed;
        }
    }
}
