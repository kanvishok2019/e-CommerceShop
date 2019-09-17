using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Query.Specifications
{

    public sealed class ShippingInvoiceSpecification : BaseSpecification<ShippingInvoice>
    {
        public ShippingInvoiceSpecification(int purchaseOrderNo)
            : base(b => b.PurchaseOrderNo == purchaseOrderNo)
        {
            AddInclude(b => b.PurchasedItems);
        }
    }
}
