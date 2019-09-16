using Infrastructure.Core.Query;
using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.Basket.Query
{
    public class GetBasketByBuyerId:IQuery<ViewModel.Basket>
    {
        public int BuyerId { get; set; }
    }
}
