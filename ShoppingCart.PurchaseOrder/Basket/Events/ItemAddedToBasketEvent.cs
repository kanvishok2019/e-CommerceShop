using System;
using System.Collections.Generic;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.Basket.Domain;

namespace ShoppingCart.ApplicationCore.Basket.Events
{
    public class ItemAddedToBasketEvent : VersionedEvent
    {
        public ItemAddedToBasketEvent(Guid id, BasketItem item)
        {
            BasketId = id;
            BasketItem = item;
        }

        public Guid BasketId { get; private set; }
        public BasketItem BasketItem { get; private set; }
    }
}