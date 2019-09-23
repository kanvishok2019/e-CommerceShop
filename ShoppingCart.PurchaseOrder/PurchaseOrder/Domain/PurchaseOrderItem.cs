using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public class PurchaseOrderItem : BaseEntity
    {
        
        public CatalogItemOrdered ItemOrdered { get;  set; }
     
        public decimal UnitPrice { get;  set; }
        public int Units { get; set; }

        public PurchaseOrderItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }

        //Note: For entity framework
        public PurchaseOrderItem() { }
    }
}
