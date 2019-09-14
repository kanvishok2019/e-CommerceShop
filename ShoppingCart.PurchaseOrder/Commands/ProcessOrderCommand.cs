﻿using System;
using Infrastructure.Core.Command;
using ShoppingCart.PurchaseOrder.Domain;

namespace ShoppingCart.PurchaseOrder.Commands
{
    public class ProcessOrderCommand : Command
    {
        public Guid BasketId { get; private set; }
        public Address Address { get; private set; }
        ProcessOrderCommand(Guid basketId, Address address)
        {
            BasketId = basketId;
            Address = address;
        }
    }
}
