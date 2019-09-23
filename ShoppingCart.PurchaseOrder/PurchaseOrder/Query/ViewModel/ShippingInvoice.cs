using System.Collections.Generic;
using System.Linq;
using Infrastructure.Core.Domain;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel
{
    public class ShippingInvoice : BaseEntity
    {
        public ShippingInvoice(int purchaseOrderNo, int buyerId, IEnumerable<PurchaseOrderItem> purchasedItems, Address shippingAddress)
        {
            PurchaseOrderNo = purchaseOrderNo;
            PurchasedItems = purchasedItems;
            BuyerId = buyerId;
            ShippingAddress = shippingAddress;
            Total = PurchasedItems.Sum(x => x.UnitPrice);
        }

        //Note: For Entity framework
        public ShippingInvoice()
        {

        }
        public int PurchaseOrderNo { get;  set; }
        public int BuyerId { get; set; }
        public IEnumerable<PurchaseOrderItem> PurchasedItems { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Total { get;  set; }

    }
}
