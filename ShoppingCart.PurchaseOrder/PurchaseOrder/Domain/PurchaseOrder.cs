using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Infrastructure.Core.Domain;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;
using ShoppingCart.ApplicationCore.PurchaseOrder.Events;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public sealed class PurchaseOrder : AggregateRoot
    {
        private readonly IDictionary<CatalogItemType, Action<List<PurchaseOrderItem>>> _processors;
        private readonly List<PurchaseOrderItem> _orderItems;

        public PurchaseOrder(Guid id, int purchaseOderNo, string buyerId, Address addressToShip,
            List<PurchaseOrderItem> items) : base(id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Default(id, nameof(id));
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            Guard.Against.Null(addressToShip, nameof(addressToShip));
            Guard.Against.Null(items, nameof(items));

            Id = id;
            PurchaseOderNo = purchaseOderNo;
            BuyerId = buyerId;
            AddressToShip = addressToShip;
            _orderItems = items;
            _processors = new Dictionary<CatalogItemType, Action<List<PurchaseOrderItem>>>();
            AddEvent(new NewPurchaseOrderCreatedEvent(id, purchaseOderNo));
        }

        public Guid Id { get; private set; }
        public int PurchaseOderNo { get; private set; }
        public DateTimeOffset OrderDate { get; } = DateTimeOffset.Now;
        public string BuyerId { get;  }
        public Address AddressToShip { get;  }
        
        public IReadOnlyCollection<PurchaseOrderItem> OrderItems => _orderItems.AsReadOnly();
        public bool IsPurchaseOrderProcessed { get; private set; }
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

            Id = newPurchaseOrderCreatedEvent.Id;
            PurchaseOderNo = newPurchaseOrderCreatedEvent.PurchaseOrderNo;
        }

        private void OnProductPurchasedEvent(ProductPurchasedEvent productPurchasedEvent)
        {
        }
        private void OnSubscriptionItemPurchasedEvent(SubscriptionItemPurchasedEvent subscriptionItemPurchasedEvent)
        {
        }

        

        private void RegisterProcessors()
        {
            _processors.Add(CatalogItemType.Subscription, SubscriptionProcessors);
            _processors.Add(CatalogItemType.Subscription, ProductProcessors);
        }

        public async Task ProcessPurchaseOrder()
        {
            await Task.Run(() =>
            {
                var catalogItemTypeList = Enum.GetValues(typeof(CatalogItemType));
                foreach (var catalogItemType in catalogItemTypeList)
                {
                    var catalogTypeEnum = (CatalogItemType)catalogItemType;
                    var processorAsync = GetProcessors(catalogTypeEnum);
                    var itemsToProcess = OrderItems.Where(x => x.ItemOrdered.CatalogItemType == catalogTypeEnum).ToList();
                    processorAsync(itemsToProcess);
                }

                IsPurchaseOrderProcessed = true;
            });

        }

        private Action<List<PurchaseOrderItem>> GetProcessors(CatalogItemType catalogItemType)
        {
            if (_processors.TryGetValue(catalogItemType, out var processor))
                return processor;
            throw new InvalidOperationException("Matching Processor not found");
        }

        private void SubscriptionProcessors(IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            AddEvent(new SubscriptionItemPurchasedEvent(purchaseOrderItem));
        }

        private void ProductProcessors(IEnumerable<PurchaseOrderItem> purchaseOrderItem)
        {
            AddEvent(new ProductPurchasedEvent(purchaseOrderItem));
        }

    }
}
