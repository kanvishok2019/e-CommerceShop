using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models
{
    public class Buyer 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SubscriptionPlan { get; set; }
    }
}
