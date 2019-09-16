using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private readonly IDictionary<int[], SubscriptionPlan> _subscriptionCatalogMapping;

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

            if (_subscriptionCatalogMapping.TryGetValue(new []{ catalogItemId }, out var newPlan))
            {
                if (existingSubscriptionPlan == null)
                    return newPlan;
                else if(existingSubscriptionPlan == newPlan)
                {
                    return newPlan;
                }
                else
                {
                    return SubscriptionPlan.PremiumClubSubscription;
                }
            }
            throw new InvalidOperationException("Subscription plan not found");
        }

        private IDictionary<int[], SubscriptionPlan> LoadMapping()
        {
            //Assumption: We will get the from some external api to avoid open closed principle break; 
            return new Dictionary<int[], SubscriptionPlan>
            {
                {new[] {1}, SubscriptionPlan.BookClubSubscription},
                {new[] {2}, SubscriptionPlan.VideoClubSubscription},
                {new[] {1, 2}, SubscriptionPlan.PremiumClubSubscription}
            };
            
        }
    }
}
