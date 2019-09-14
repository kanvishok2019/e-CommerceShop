using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Infrastructure.Core.Domain;
using Infrastructure.Core.Event;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public sealed class PurchaseOrder : AggregateRoot
    {
        public PurchaseOrder(Guid id, string buyerId, Address shipToAddress,
            List<PurchaseOrderItem> items) : base(id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Default(id, nameof(id));
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            Guard.Against.Null(shipToAddress, nameof(shipToAddress));
            Guard.Against.Null(items, nameof(items));

            AddEvent(new NewPurchaseOrderCreatedEvent(id));
        }

        public Guid Id { get; private set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public string BuyerId { get; private set; }
        public Address ShipToAddress { get; private set; }

        protected override void RegisterUpdateHandlers()
        {
            RegisterUpdateHandler<NewPurchaseOrderCreatedEvent>(OnNewPurchaseOrderCreated);
        }

        private void OnNewPurchaseOrderCreated(NewPurchaseOrderCreatedEvent newPurchaseOrderCreatedEvent)
        {
            if (newPurchaseOrderCreatedEvent == null)
            {
                return;
            }

            Id = newPurchaseOrderCreatedEvent.Id;
        }


        //public async Task ProcessOrderAsync(Guid basketId, Address shippingAddress)
        //{

        //}

    }
    public class NewPurchaseOrderCreatedEvent : VersionedEvent
    {
        public Guid Id { get; }
        public NewPurchaseOrderCreatedEvent(Guid id)
        {
            Id = id;
        }
    }
}
