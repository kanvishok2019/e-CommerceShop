using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShoppingCart.ApplicationCoreTests.Basket
{
    public class BasketTests
    {
        [Fact]
        public void BasketCreatedEvent_Should_Raised_When_Basket_Created()
        {
            var basket = new ApplicationCore.Basket.Domain.Basket(Guid.NewGuid(), 12345);
            var eventObj = basket.UnCommittedEvents.FirstOrDefault();
            Assert.Single(basket.UnCommittedEvents);
            Assert.Equal("BasketCreatedEvent", eventObj.GetType().Name);
        }

        [Fact]
        public void Item_Should_Added_To_Basket_When_Add_To_Basket()
        {
            var basket = new ApplicationCore.Basket.Domain.Basket(Guid.NewGuid(), 12345);
            basket.AddItem(1,5.0M,1);
            Assert.Single(basket.Items);
        }

        [Fact]
        public void Item_Quantity_Should_Get_Updated_When_Added_Existing_Item()
        {
            var basket = new ApplicationCore.Basket.Domain.Basket(Guid.NewGuid(), 12345);
            basket.AddItem(1, 5.0M, 1);
            basket.AddItem(1, 5.0M, 1);
            Assert.Equal(1,basket.Items.Count);
            Assert.Equal(2, basket.Items.First(i=>i.CatalogItemId==1).Quantity);
        }

        [Fact]
        public void Only_One_Item_Should_Get_Added_To_Basket()
        {
            var basket = new ApplicationCore.Basket.Domain.Basket(Guid.NewGuid(), 12345);
            basket.AddItem(1,5M);
            Assert.Equal(1, basket.Items.First(i => i.CatalogItemId == 1).Quantity);
        }

        [Fact]
        public void Keep_The_Item_Price_Same_When_The_More_Same_Items_Added()
        {
            var basket = new ApplicationCore.Basket.Domain.Basket(Guid.NewGuid(), 12345);
            basket.AddItem(1, 5M,1);
            basket.AddItem(1, 5M, 1);
            Assert.Equal(5M, basket.Items.First(i => i.CatalogItemId == 1).UnitPrice);
        }

    }
}
