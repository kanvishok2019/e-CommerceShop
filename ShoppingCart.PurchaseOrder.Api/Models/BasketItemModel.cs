using System;

namespace ShoppingCart.Api.Models
{
    public class BasketItemModel
    {
        public Guid BasketId { get; set; }
        public int CatalogItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
