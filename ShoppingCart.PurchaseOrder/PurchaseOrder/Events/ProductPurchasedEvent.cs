using System.Collections.Generic;
using System.Linq;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class ProductPurchasedEvent : VersionedEvent
    {
        public readonly List<PurchaseOrderItem> PurchaseOrderList;
        public int PurchaseOrderNo { get;  }
        public int BuyerId { get; }
        public bool IsPurchaseOrderProcessed { get;  }
        public Address ShippingAddress { get; }

        public ProductPurchasedEvent(int purchasePurchaseOrderNo, int buyerId, IEnumerable<PurchaseOrderItem> purchaseOrderItem,bool isPurchaseOrderProcessed, Address shippingAddress)
        {
            PurchaseOrderNo = purchasePurchaseOrderNo;
            PurchaseOrderList = purchaseOrderItem.ToList();
            BuyerId = buyerId;
            IsPurchaseOrderProcessed = isPurchaseOrderProcessed;
            ShippingAddress = shippingAddress;
        }
    }
}