using Infrastructure.Core.Query;

namespace ShoppingCart.ApplicationCore.Basket.Query
{
    public class GetBasketByBuyerId:IQuery<ViewModel.Basket>
    {
        public int BuyerId { get; set; }
    }
}
