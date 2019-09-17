using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public class PurchaseOrderIdNumberMapping : BaseEntity
    {
        public Guid PurchaseOrderId { get; set; }
    }
}
