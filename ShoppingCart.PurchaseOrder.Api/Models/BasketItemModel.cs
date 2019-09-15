using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models
{
    public class BasketItemModel
    {
        public Guid BasketId { get; set; }
        public Guid CatalogItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
