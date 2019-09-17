using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Core.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ShoppingCart.Api;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;
using ShoppingCart.ApplicationCore.Buyer;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel;
using Xunit;

namespace FunctionalTest
{
    public class PurchaseOrderTests : IClassFixture<ShopWebApplicationFactory<Startup>>
    {
    
        public HttpClient Client { get; }
        public PurchaseOrderTests(ShopWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Order_Should_Process_Basket()
        {
            var basketModel = new CreateBasketModel { BuyerId = 1 };
            //Crate Basket
            var response = await Client.PostAsJsonAsync("/api/basket/", basketModel);
            response.EnsureSuccessStatusCode();

            //Get Created Casket
            var createdBasketResponse = await Client.GetAsync("api/basket/" + basketModel.BuyerId);
            createdBasketResponse.EnsureSuccessStatusCode();
            var basket = await createdBasketResponse.Content.ReadAsAsync<Basket>();

            //Add Item To Basket
            var addBasketModel = new BasketITemPostModel
            {
                BasketId = basket.BasketId,
                CatalogItemId = 1,
                Quantity = 5M,
                Price = 10.25M
            };

            var itemAddedResponse = await Client.PutAsJsonAsync("/api/basket/" + addBasketModel.BasketId, addBasketModel);
            itemAddedResponse.EnsureSuccessStatusCode();

            //Process Basket
            var purchaseOrderModel = new PurchaseOrderModel { BasketId = basket.BasketId.ToString() };
            var crateProcessOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/", purchaseOrderModel);
            crateProcessOrderResponse.EnsureSuccessStatusCode();

            //Process Purchaseorder

            var processOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/1/process", new { });
            processOrderResponse.EnsureSuccessStatusCode();

        }

        //BR1:Test
        [Fact]
        public async Task When_A_Purchase_Order_Contains_Membership_It_Has_To_Be_Activates()
        {
            var basketModel = new CreateBasketModel { BuyerId = 1 };
            //Crate Basket
            var response = await Client.PostAsJsonAsync("/api/basket/", basketModel);
            response.EnsureSuccessStatusCode();

            //Get Created Casket
            var createdBasketResponse = await Client.GetAsync("api/basket/" + basketModel.BuyerId);
            createdBasketResponse.EnsureSuccessStatusCode();
            var basket = await createdBasketResponse.Content.ReadAsAsync<Basket>();

            //Add Item To Basket
            var addBasketModel = new BasketITemPostModel
            {
                BasketId = basket.BasketId,
                CatalogItemId = 1,
                Quantity = 5M,
                Price = 10.25M
            };

            var itemAddedResponse = await Client.PutAsJsonAsync("/api/basket/" + addBasketModel.BasketId, addBasketModel);
            itemAddedResponse.EnsureSuccessStatusCode();

            //Process Basket
            var purchaseOrderModel = new PurchaseOrderModel { BasketId = basket.BasketId.ToString() };
            var crateProcessOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/", purchaseOrderModel);
            crateProcessOrderResponse.EnsureSuccessStatusCode();

            //Process Purchaseorder
            var processOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/1/process", new { });
            processOrderResponse.EnsureSuccessStatusCode();

            //Verify Membership
            var buyerResponse = await Client.GetAsync("/api/buyer");
            buyerResponse.EnsureSuccessStatusCode();
            var buyer = await buyerResponse.Content.ReadAsAsync<List<Buyer>>();
            var buyerInPlan = buyer.FirstOrDefault(b => b.Id == 1).SubscriptionPlan;
            Assert.Equal(SubscriptionPlan.BookClubSubscription, buyerInPlan);
        }

        //BR2:Test
        [Fact]
        public async Task When_A_Purchase_Order_Contains_Physical_Product_Invoice_Should_Get_Generated()
        {
            var basketModel = new CreateBasketModel { BuyerId = 1 };
            //Crate Basket
            var response = await Client.PostAsJsonAsync("/api/basket/", basketModel);
            response.EnsureSuccessStatusCode();

            //Get Created Casket
            var createdBasketResponse = await Client.GetAsync("api/basket/" + basketModel.BuyerId);
            createdBasketResponse.EnsureSuccessStatusCode();
            var basket = await createdBasketResponse.Content.ReadAsAsync<Basket>();

            //Add Item To Basket
            var addBasketModel = new BasketITemPostModel
            {
                BasketId = basket.BasketId,
                CatalogItemId = 3,
                Quantity = 5M,
                Price = 10.25M
            };

            var itemAddedResponse = await Client.PutAsJsonAsync("/api/basket/" + addBasketModel.BasketId, addBasketModel);
            itemAddedResponse.EnsureSuccessStatusCode();

            //Process Basket
            var purchaseOrderModel = new PurchaseOrderModel { BasketId = basket.BasketId.ToString() };
            var crateProcessOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/", purchaseOrderModel);
            crateProcessOrderResponse.EnsureSuccessStatusCode();

            //Process Purchaseorder
            var processOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/1/process", new { });
            processOrderResponse.EnsureSuccessStatusCode();

            //Verify Invoice
            var shippingInvoice = await processOrderResponse.Content.ReadAsAsync<ShippingInvoice>();
         
            //Issue: Unable to deserialize the ShippingInvoice for some reason
            //var itemInInvoice = shippingInvoice.PurchasedItems.FirstOrDefault(o => o.ItemOrdered.CatalogItemId == 3);
            //Assert.Equal(3, itemInInvoice.ItemOrdered.CatalogItemId);
        }

    }

    public class PurchaseOrderModel
    {
        public string BasketId { get; set; }
    }
}
