using Ardalis.GuardClauses;
using Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.ApplicationCore.Basket.Events;

namespace ShoppingCart.ApplicationCore.Basket.Domain
{
    public sealed class Basket : AggregateRoot
    {
        public Basket(Guid id, string buyerId) : base(id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Default(id, nameof(id));
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            AddEvent(new BasketCreatedEvent(id, buyerId));

        }

        public Guid Id { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
        {
            if (Items.All(item => item.CatalogItemId != catalogItemId))
            {
                _items.Add(new BasketItem
                {
                    CatalogItemId = catalogItemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }

            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            existingItem.Quantity += quantity;
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
            Id = basketCreatedEvent.Id;
            BuyerId = basketCreatedEvent.BuyerId;

        }
    }
}
