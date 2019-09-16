using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class SubscriptionItemPurchasedEvent : VersionedEvent
    {
        public readonly IEnumerable<PurchaseOrderItem> PurchaseOrderList;
        public int PurchaseOrderNo { get; }
        public int BuyerId { get; }
        public SubscriptionItemPurchasedEvent(int purchasePurchaseOrderNo, int buyerId, IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            PurchaseOrderNo = purchasePurchaseOrderNo;
            BuyerId = buyerId;
            PurchaseOrderList = purchaseOrderItem;
        }
    }
}
