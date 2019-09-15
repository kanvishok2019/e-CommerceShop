using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Repository;

namespace ShoppingCart.ApplicationCore.Basket.Query.Specifications
{
    public sealed class BasketWithItemsSpecification : BaseSpecification<ViewModel.Basket>
    {
        public BasketWithItemsSpecification(int basketId)
            : base(b => b.Id == basketId)
        {
            AddInclude(b => b.BasketItems);
        }
        public BasketWithItemsSpecification(string buyerId)
            : base(b => b.BuyerId == buyerId)
        {
            AddInclude(b => b.BasketItems);
        }
    }
}
