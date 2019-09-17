using System;
using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.Basket.Query.ViewModel
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
        public Guid BasketId { get; set; }
    }
}
