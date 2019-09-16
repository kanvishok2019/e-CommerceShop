using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.Buyer
{
    public class Buyer : BaseEntity
    {
        public SubscriptionPlan? SubscriptionPlan { get; set; }
    }
}