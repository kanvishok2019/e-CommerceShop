using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Query;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Query
{
    public class ShippingInvoiceQuery: IQuery<ShippingInvoice>
    {
        public ShippingInvoiceQuery(int purchaseOrderNo)
        {
            PurchaseOrderNo = purchaseOrderNo;
        }

        public int PurchaseOrderNo { get; }
    }
}
