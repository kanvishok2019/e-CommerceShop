using System;
using System.Collections.Generic;
using System.Text;
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

            var basket = new Domain.Basket(Guid.NewGuid(), "buyerId");
            await _shopAggregateRepositoryService.SaveAsync(basket);
        }
    }
}
