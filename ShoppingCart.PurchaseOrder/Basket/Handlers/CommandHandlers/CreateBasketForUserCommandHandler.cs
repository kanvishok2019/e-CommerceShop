using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using ShoppingCart.ApplicationCore.Basket.Commands;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.CommandHandlers
{
    public class CreateBasketForUserCommandHandler: ICommandHandler<CreateBasketForUserCommand>
    {
        public Task HandleAsync(CreateBasketForUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
