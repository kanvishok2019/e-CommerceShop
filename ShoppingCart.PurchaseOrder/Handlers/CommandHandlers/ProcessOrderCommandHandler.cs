using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using ShoppingCart.PurchaseOrder.Commands;

namespace ShoppingCart.PurchaseOrder.Handlers.CommandHandlers
{
    public class ProcessOrderCommandHandler:ICommandHandler<ProcessOrderCommand>
    {
        public Task HandleAsync(ProcessOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
