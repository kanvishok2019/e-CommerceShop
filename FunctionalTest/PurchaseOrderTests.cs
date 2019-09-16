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
        private readonly CreateBasketModel _basketModel;
        
        public HttpClient Client { get; }
        public PurchaseOrderTests(ShopWebApplicationFactory<Startup> factory)
        {
            _basketModel = new CreateBasketModel { BuyerId = 12345 };
            
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Order_Should_Process_Basket()
        {
            //Crate Basket
            var response = await Client.PostAsJsonAsync("/api/basket/", _basketModel);
            response.EnsureSuccessStatusCode();

            //Get Created Casket
            var createdBasketResponse = await Client.GetAsync("api/basket/" + _basketModel.BuyerId);
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
            //var afterItemAddedResponse = await Client.GetAsync("api/basket/" + _basketModel.BuyerId);
            //afterItemAddedResponse.EnsureSuccessStatusCode();
            //basket = await afterItemAddedResponse.Content.ReadAsAsync<Basket>();

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
