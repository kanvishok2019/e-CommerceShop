using System;
using System.Collections.Generic;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Events
{
    public class NewPurchaseOrderCreatedEvent : VersionedEvent
    {
        public Guid PurchaseOrderId { get; }
        public int PurchaseOrderNo { get; }
        public IEnumerable<PurchaseOrderItem> PurchaseOrderItems { get; }
        public int BuyerId { get; }
        public Address AddressToShip { get; }
        public NewPurchaseOrderCreatedEvent(int buyerId, Guid purchaseOrderId, int purchaseOrderNo, IEnumerable<PurchaseOrderItem> purchaseOrderItems, Address addressToShip)
        {
            BuyerId = buyerId;
            PurchaseOrderId = purchaseOrderId;
            PurchaseOrderNo = purchaseOrderNo;
            PurchaseOrderItems = purchaseOrderItems;
            AddressToShip = addressToShip;
        }
    }
}