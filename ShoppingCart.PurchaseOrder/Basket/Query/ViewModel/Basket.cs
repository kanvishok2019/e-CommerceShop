using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Domain;
 
namespace ShoppingCart.ApplicationCore.Basket.Query.ViewModel
{
    public class Basket:BaseEntity
    {
        public Guid BasketId { get; set; }
        public int BuyerId { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
    }
}
