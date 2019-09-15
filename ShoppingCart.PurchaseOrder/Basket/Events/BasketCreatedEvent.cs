using System;
using Infrastructure.Core.Event;

namespace ShoppingCart.ApplicationCore.Basket.Events
{
    public class BasketCreatedEvent : VersionedEvent
    {
        public BasketCreatedEvent(Guid basketId, string buyerId)
        {
            BasketId = basketId;
            BuyerId = buyerId;
        }
        public Guid BasketId { get; }
        public string BuyerId { get; }

    }
}