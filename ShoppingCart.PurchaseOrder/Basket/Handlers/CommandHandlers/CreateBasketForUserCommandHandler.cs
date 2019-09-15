using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Commands;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.CommandHandlers
{
    public class CreateBasketForUserCommandHandler: ICommandHandler<CreateBasketForUserCommand>
    {
        private readonly IAggregateRepositoryService<Domain.Basket> _shopAggregateRepositoryService;

        public CreateBasketForUserCommandHandler(IAggregateRepositoryService<Domain.Basket> shopAggregateRepositoryService)
        {
            _shopAggregateRepositoryService = shopAggregateRepositoryService;
        }

        public async Task HandleAsync(CreateBasketForUserCommand command)
        {
            var basket = new Domain.Basket(Guid.NewGuid(), command.BuyerId);
            await _shopAggregateRepositoryService.SaveAsync(basket);
        }
    }
}
