using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.Buyer
{
    public class Buyer : BaseEntity
    {
        private SubscriptionPlan subscriptionPlan { get; set; }
    }
