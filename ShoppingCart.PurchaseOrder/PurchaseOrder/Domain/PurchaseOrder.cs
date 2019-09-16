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
        public PurchaseOrder(Guid id, int purchaseOderNo, string buyerId, Address shipToAddress,
            List<PurchaseOrderItem> items) : base(id)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Default(id, nameof(id));
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            Guard.Against.Null(shipToAddress, nameof(shipToAddress));
            Guard.Against.Null(items, nameof(items));

            AddEvent(new NewPurchaseOrderCreatedEvent(id, purchaseOderNo));
        }

        public Guid Id { get; private set; }
        public int PurchaseOderNo { get; private set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public string BuyerId { get; private set; }
        public Address ShipToAddress { get; private set; }
        private readonly List<PurchaseOrderItem> _orderItems = new List<PurchaseOrderItem>();
        public IReadOnlyCollection<PurchaseOrderItem> OrderItems => _orderItems.AsReadOnly();
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
            PurchaseOderNo = newPurchaseOrderCreatedEvent.PurchaseOrderNo;
        }

        private IDictionary<CatalogItemType, Action<List<PurchaseOrderItem>>> _processors = new Dictionary<CatalogItemType, Action<List<PurchaseOrderItem>>>();

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
