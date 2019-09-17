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
            Total = 100; //PurchasedItems.Sum(x => x.UnitPrice);
        }

        //Note: For Entity framework
        public ShippingInvoice()
        {

        }
        public int PurchaseOrderNo { get; }
        public int BuyerId { get; }
        public IEnumerable<PurchaseOrderItem> PurchasedItems { get; }
        public Address ShippingAddress { get; }
        public decimal Total { get; }

    }
}
