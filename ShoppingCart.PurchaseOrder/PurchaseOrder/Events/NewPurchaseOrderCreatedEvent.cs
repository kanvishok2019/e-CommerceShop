using System;
using Infrastructure.Core.Event;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class NewPurchaseOrderCreatedEvent : VersionedEvent
    {
        public Guid PurchaseOrderId { get; }
        public int PurchaseOrderNo { get; }
        public NewPurchaseOrderCreatedEvent(Guid id, int purchaseOrderNo)
        {
            PurchaseOrderId = id;
            PurchaseOrderNo = purchaseOrderNo;
        }
    }
}