using Infrastructure.Core.Command;

namespace ShoppingCart.ApplicationCore.Basket.Commands
{
    public class CreateBasketForUserCommand : Command
    {
        public CreateBasketForUserCommand(string buyerId)
        {
            BuyerId = buyerId;
        }

        public string BuyerId { get; private set; }

    }
}
