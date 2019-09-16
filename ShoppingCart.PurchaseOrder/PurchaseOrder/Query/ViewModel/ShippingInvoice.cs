using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Domain;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel
{
    public class ShippingInvoice : BaseEntity
    {
        public ShippingInvoice(int purchaseOrderNo, IReadOnlyCollection<PurchaseOrderItem> purchasedItems)
        {
            PurchaseOrderNo = purchaseOrderNo;
            PurchasedItems = purchasedItems;
        }

        public int PurchaseOrderNo { get; set; }
        public IReadOnlyCollection<PurchaseOrderItem> PurchasedItems { get; }

    }
}
