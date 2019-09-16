using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Domain;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel
{
    public class ShippingInvoice : BaseEntity
    {
        public ShippingInvoice(int purchaseOrderNo, int buyerId, IEnumerable<PurchaseOrderItem> purchasedItems)
        {
            PurchaseOrderNo = purchaseOrderNo;
            PurchasedItems = purchasedItems;
            BuyerId = buyerId;
        }

        //Note: For Entity framework
        public ShippingInvoice()
        {

        }
        public int PurchaseOrderNo { get; }
        public int BuyerId { get; }
        public IEnumerable<PurchaseOrderItem> PurchasedItems { get; }

    }
}
