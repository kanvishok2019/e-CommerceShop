using System;
using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public class PurchaseOrderIdNumberMapping : BaseEntity
    {
        public Guid PurchaseOrderId { get; set; }
    }
}
