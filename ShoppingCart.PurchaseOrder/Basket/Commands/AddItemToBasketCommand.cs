using Infrastructure.Core.Command;
using System;

namespace ShoppingCart.ApplicationCore.Basket.Commands
{
    public class AddItemToBasketCommand : Command
    {
        public Guid BasketId { get; private set; }
        public int CatalogItemId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        AddItemToBasketCommand(Guid basketId, int catalogItemId, decimal price, int quantity)
        {
            BasketId = basketId;
            CatalogItemId = catalogItemId;
            Quantity = quantity;
            Price = price;
        }
    }
}