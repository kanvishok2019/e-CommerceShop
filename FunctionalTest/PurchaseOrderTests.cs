using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ShoppingCart.Api;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;
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
            var basketModel = new CreateBasketModel { BuyerId = 5 }; 
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
                CatalogItemId = 2,
                Quantity = 5M,
                Price = 10.25M
            };

            var itemAddedResponse = await Client.PutAsJsonAsync("/api/basket/" + addBasketModel.BasketId, addBasketModel);
            itemAddedResponse.EnsureSuccessStatusCode();
           
            //Process Basket
            var purchaseOrderModel = new PurchaseOrderModel{BasketId = basket.BasketId.ToString()};
            var crateProcessOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/", purchaseOrderModel);
            crateProcessOrderResponse.EnsureSuccessStatusCode();

            //Process Purchaseorder
        
            var processOrderResponse = await Client.PostAsJsonAsync("/api/purchase-orders/1/process",new{});
            processOrderResponse.EnsureSuccessStatusCode();

            
        }
    }

    public class PurchaseOrderModel
    {
        public string BasketId { get; set; }
    }
}
