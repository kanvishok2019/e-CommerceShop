using System.Threading.Tasks;
using Infrastructure.Core.Command;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Commands;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.CommandHandlers
{
    public class AddItemToBasketHandler : ICommandHandler<AddItemToBasketCommand>
    {
        private readonly IAggregateRepositoryService<Domain.Basket> _shopAggregateRepositoryService;

        public AddItemToBasketHandler(IAggregateRepositoryService<Domain.Basket> basketAggregateRepositoryService)
        {
            _shopAggregateRepositoryService = basketAggregateRepositoryService;
        }

        public async Task HandleAsync(AddItemToBasketCommand command)
        {
            //Assumptions: Stock Of Product is Available
            var basket = await _shopAggregateRepositoryService.GetAsync(command.BasketId);
            basket.AddItem(command.CatalogItemId, command.Price, command.Quantity);
            await _shopAggregateRepositoryService.SaveAsync(basket);

        }
    }
}
