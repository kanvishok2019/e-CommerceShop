using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Domain;

namespace ShoppingCart.PurchaseOrder.Domain
{
    public class PurchaseOrderItem : BaseEntity
    {
        public CatalogItemOrdered ItemOrdered { get; private set; }
     
        public decimal UnitPrice { get; private set; }
        public int Units { get; set; }

        public PurchaseOrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }
    }
}
