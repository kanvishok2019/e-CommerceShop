using System;
using Infrastructure.Core.Command;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Commands
{
    public class CreatePurchaseOrderCommand : Command
    {
        public Guid BasketId { get; }
        public Address Address { get; }
        public CreatePurchaseOrderCommand(Guid basketId, Address address)
        {
            BasketId = basketId;
            Address = address;
        }
    }
}
