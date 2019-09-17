using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.Buyer
{
    public class Buyer : BaseEntity
    {
        public string Name { get; set; }
        public SubscriptionPlan? SubscriptionPlan { get; set; }
    }
}