using System;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.CommandHandlers
{
    public class ProcessOrderCommandHandler:ICommandHandler<ProcessOrderCommand>
    {
        public Task HandleAsync(ProcessOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
