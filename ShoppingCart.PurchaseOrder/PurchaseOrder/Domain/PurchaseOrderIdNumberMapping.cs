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
        
        
        //private int _purchaseOrderNo;
        public Guid PurchaseOrderId { get; set; }

        //public int PurchaseOrderNo => Id;
        //{
        //    get { return _purchaseOrderNo; }
        //    set => _purchaseOrderNo = Id;
        //}
    }
}
