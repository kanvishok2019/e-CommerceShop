using Ardalis.GuardClauses;
using Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.Basket.Events;

namespace ShoppingCart.ApplicationCore.Basket.Domain
{
    public sealed class Basket : AggregateRoot
    {
        public Basket(Guid id, int buyerId) : base(id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Default(id, nameof(id));
            Guard.Against.Default(buyerId, nameof(buyerId));
            AddEvent(new BasketCreatedEvent(id, buyerId));

        }
        public Basket(Guid id, IEnumerable<IVersionedEvent> eventsHistory) : base(id)
        {
            ApplyUpdate(eventsHistory);
        }
        public Guid Id { get; private set; }
        public int BuyerId { get; private set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
        {
            if (Items.All(item => item.CatalogItemId != catalogItemId))
            {
                var item = new BasketItem
                {
                    CatalogItemId = catalogItemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    BasketId = Id
                };
                _items.Add(item);
                AddEvent(new ItemAddedToBasketEvent(Id, item));
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            if (existingItem != null) existingItem.Quantity += quantity;
        }


        protected override void RegisterUpdateHandlers()
        {
            RegisterUpdateHandler<BasketCreatedEvent>(OnBasketCreatedEvent);
        }

        private void OnBasketCreatedEvent(BasketCreatedEvent basketCreatedEvent)
        {
            if (basketCreatedEvent == null)
            {
                return;
            }
            Id = basketCreatedEvent.BasketId;
            BuyerId = basketCreatedEvent.BuyerId;
        }

        private void OnItemAddedToBasketEvent(ItemAddedToBasketEvent itemAddedToBasketEvent)
        {
            if (itemAddedToBasketEvent == null)
                return;
            _items.Add(itemAddedToBasketEvent.BasketItem);
        }
    }
}
