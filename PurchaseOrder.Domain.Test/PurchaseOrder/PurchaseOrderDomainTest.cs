using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;
using Xunit;

namespace ShoppingCart.ApplicationCoreTests.PurchaseOrder
{
    public class PurchaseOrderDomainTest
    {
        [Fact]
        public void Purchase_Order_WithOut_Proper_Id_Should_Throw_ArgumentNullException()
        {
            //Assert.Throws<ArgumentNullException>(() => new ShoppingCart.PurchaseOrder.Domain.PurchaseOrder(new Guid()));
        }

        [Fact]
        public void NewPurchaseOrderCreatedEvent_Should_Raised_When_Purchase_Order_Created()
        {
            var address = new Address("street", "city", "state", "country", "zipcode");
            var purchasedItem = new List<PurchaseOrderItem>()
            {
                new PurchaseOrderItem(
                    new CatalogItemOrdered(1, CatalogItemType.Subscription,
                        "ProductName", "PictureUri"), 5.0M, 5)
            };

            var purchaseOrder = new ApplicationCore.PurchaseOrder.Domain.PurchaseOrder(Guid.NewGuid(),123,12345, address, purchasedItem);
            var eventObj = purchaseOrder.UnCommittedEvents.FirstOrDefault();
            Assert.Equal("NewPurchaseOrderCreatedEvent", eventObj.GetType().Name);
            Assert.Single(purchaseOrder.UnCommittedEvents);
        }
    }
}
