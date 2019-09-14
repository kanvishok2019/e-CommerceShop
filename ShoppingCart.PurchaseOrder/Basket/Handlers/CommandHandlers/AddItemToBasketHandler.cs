using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using ShoppingCart.ApplicationCore.Basket.Commands;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.CommandHandlers
{
    public class AddItemToBasketHandler:ICommandHandler<AddItemToBasketCommand>
    {
        public Task HandleAsync(AddItemToBasketCommand command)
        {
            //Assumptions: Stock Of Product is Available
            return Task.FromResult("");
        }
    }
}
