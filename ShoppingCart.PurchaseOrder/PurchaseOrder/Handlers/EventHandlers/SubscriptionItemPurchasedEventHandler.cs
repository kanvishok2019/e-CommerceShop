using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Event;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Buyer;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;
using ShoppingCart.ApplicationCore.PurchaseOrder.Events;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.EventHandlers
{
    public class SubscriptionItemPurchasedEventHandler : IEventHandler<SubscriptionItemPurchasedEvent>
    {
        private readonly IAsyncRepository<Buyer.Buyer> _buyerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDictionary<SubscriptionPlan, int[]> _subscriptionCatalogMapping;

        public SubscriptionItemPurchasedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _buyerRepository = _unitOfWork.GetRepositoryAsync<Buyer.Buyer>();
            _subscriptionCatalogMapping = LoadMapping();
        }

        public async Task HandleAsync(SubscriptionItemPurchasedEvent @event)
        {
            var buyer = await _buyerRepository.GetByIdAsync(@event.BuyerId);
            foreach (var purchaseOrderItem in @event.PurchaseOrderList)
            {
                buyer.SubscriptionPlan = GetPlan(purchaseOrderItem.ItemOrdered.CatalogItemId, buyer.SubscriptionPlan);
            }

            await _unitOfWork.SaveChangesAsync();

        }

        private SubscriptionPlan GetPlan(int catalogItemId, SubscriptionPlan? existingSubscriptionPlan)
        {
            if (existingSubscriptionPlan != null)
            {
                if (_subscriptionCatalogMapping.TryGetValue(existingSubscriptionPlan.Value, out var purchasedProduct))
                {
                    purchasedProduct = purchasedProduct.Append(catalogItemId).OrderBy(x => x).ToArray();
                    
                    var newPlanMapping = _subscriptionCatalogMapping.FirstOrDefault(x => x.Value.SequenceEqual(purchasedProduct));

                    return newPlanMapping.Key;
                }
            }
            else
            {
                var newPlanMapping = _subscriptionCatalogMapping.FirstOrDefault(x => x.Value.SequenceEqual(new[] { catalogItemId }));
                return newPlanMapping.Key;
            }
            
            throw new InvalidOperationException("Subscription plan not found");
        }

        private IDictionary<SubscriptionPlan, int[]> LoadMapping()
        {
            //Assumption: We will get the from some external api to avoid open closed principle break; 
            return new Dictionary<SubscriptionPlan, int[]>
            {
                {SubscriptionPlan.BookClubSubscription,new[] {1}},
                {SubscriptionPlan.VideoClubSubscription, new[] {2} },
                {SubscriptionPlan.PremiumClubSubscription,new[] {1, 2}}
            };

        }
    }
}
