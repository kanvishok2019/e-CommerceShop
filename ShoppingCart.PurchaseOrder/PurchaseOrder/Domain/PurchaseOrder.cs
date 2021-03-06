﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Infrastructure.Core.Domain;
using Infrastructure.Core.Event;
using ShoppingCart.ApplicationCore.PurchaseOrder.Events;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public sealed class PurchaseOrder : AggregateRoot
    {
        private readonly IDictionary<CatalogItemType, Action<List<PurchaseOrderItem>>> _processors;
        private List<PurchaseOrderItem> _orderItems;
        public Guid Id { get; private set; }
        public int PurchaseOderNo { get; private set; }
        public DateTimeOffset OrderDate { get; } = DateTimeOffset.Now;
        public int BuyerId { get; private set; }
        public Address AddressToShip { get; private set; }
        public IReadOnlyCollection<PurchaseOrderItem> OrderItems => _orderItems.AsReadOnly();
        public bool IsPurchaseOrderProcessed { get; private set; }

        public PurchaseOrder(Guid id, int purchaseOderNo, int buyerId, Address addressToShip,
            List<PurchaseOrderItem> items) : base(id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Default(id, nameof(id));
            Guard.Against.Default(buyerId, nameof(buyerId));
            Guard.Against.Null(addressToShip, nameof(addressToShip));
            Guard.Against.Null(items, nameof(items));

            Id = id;
            PurchaseOderNo = purchaseOderNo;
            BuyerId = buyerId;
            AddressToShip = addressToShip;
            _orderItems = items;
            _processors = new Dictionary<CatalogItemType, Action<List<PurchaseOrderItem>>>();
            AddEvent(new NewPurchaseOrderCreatedEvent(BuyerId, id, purchaseOderNo, _orderItems, addressToShip));
        }

        public PurchaseOrder(Guid id, IEnumerable<IVersionedEvent> eventsHistory) : base(id)
        {
            _orderItems = new List<PurchaseOrderItem>();
            _processors = new Dictionary<CatalogItemType, Action<List<PurchaseOrderItem>>>();
            ApplyUpdate(eventsHistory);
        }
        public async Task ProcessPurchaseOrder()
        {
            RegisterProcessors();
            await Task.Run(() =>
            {
                var catalogItemTypeList = Enum.GetValues(typeof(CatalogItemType));
                foreach (var catalogItemType in catalogItemTypeList)
                {
                    var catalogTypeEnum = (CatalogItemType)catalogItemType;
                    var processorAsync = GetProcessors(catalogTypeEnum);
                    var itemsToProcess = OrderItems.Where(x => x.ItemOrdered.CatalogItemType == catalogTypeEnum).ToList();
                    if (itemsToProcess.Count > 0)
                        processorAsync(itemsToProcess);
                }

                IsPurchaseOrderProcessed = true;
            });
        }

        protected override void RegisterUpdateHandlers()
        {
            RegisterUpdateHandler<NewPurchaseOrderCreatedEvent>(OnNewPurchaseOrderCreated);
            RegisterUpdateHandler<ProductPurchasedEvent>(OnProductPurchasedEvent);
            RegisterUpdateHandler<SubscriptionItemPurchasedEvent>(OnSubscriptionItemPurchasedEvent);
        }

        private void OnNewPurchaseOrderCreated(NewPurchaseOrderCreatedEvent newPurchaseOrderCreatedEvent)
        {
            if (newPurchaseOrderCreatedEvent == null)
            {
                return;
            }

            Id = newPurchaseOrderCreatedEvent.PurchaseOrderId;
            BuyerId = newPurchaseOrderCreatedEvent.BuyerId;
            PurchaseOderNo = newPurchaseOrderCreatedEvent.PurchaseOrderNo;
            _orderItems = newPurchaseOrderCreatedEvent.PurchaseOrderItems.ToList();
            AddressToShip = newPurchaseOrderCreatedEvent.AddressToShip;
        }

        private void OnProductPurchasedEvent(ProductPurchasedEvent productPurchasedEvent)
        {
            IsPurchaseOrderProcessed = productPurchasedEvent.IsPurchaseOrderProcessed;
        }
        private void OnSubscriptionItemPurchasedEvent(SubscriptionItemPurchasedEvent subscriptionItemPurchasedEvent)
        {
            IsPurchaseOrderProcessed = subscriptionItemPurchasedEvent.IsPurchaseOrderProcessed;
        }

        private void RegisterProcessors()
        {
            _processors.Add(CatalogItemType.Subscription, SubscriptionProcessors);
            _processors.Add(CatalogItemType.Product, ProductProcessors);
        }

        private Action<List<PurchaseOrderItem>> GetProcessors(CatalogItemType catalogItemType)
        {
            if (_processors.TryGetValue(catalogItemType, out var processor))
                return processor;
            throw new InvalidOperationException("Matching Processor not found");
        }

        private void SubscriptionProcessors(IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            AddEvent(new SubscriptionItemPurchasedEvent(PurchaseOderNo, BuyerId, purchaseOrderItem, true));
        }

        private void ProductProcessors(IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            AddEvent(new ProductPurchasedEvent(PurchaseOderNo, BuyerId, purchaseOrderItem, true, AddressToShip));
        }

    }
}
