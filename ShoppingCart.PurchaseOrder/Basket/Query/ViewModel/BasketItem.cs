using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Infrastructure.Core.Domain;
 
namespace ShoppingCart.ApplicationCore.Basket.Query.ViewModel
{
    public class BasketItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
        public int BasketId { get; private set; }
    }
}
