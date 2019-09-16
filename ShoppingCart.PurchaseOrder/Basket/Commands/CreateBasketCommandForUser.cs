using Infrastructure.Core.Command;

namespace ShoppingCart.ApplicationCore.Basket.Commands
{
    public class CreateBasketForUserCommand : Command
    {
        public CreateBasketForUserCommand(int buyerId)
        {
            BuyerId = buyerId;
        }

        public int BuyerId { get; private set; }

    }
}
