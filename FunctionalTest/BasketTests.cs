using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using ShoppingCart.Api;
using Microsoft.AspNetCore.Http.Extensions;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;
using Xunit;

namespace FunctionalTest
{
    public class BasketTests : IClassFixture<ShopWebApplicationFactory<Startup>>
    {
        public BasketTests(ShopWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Create_New_Basket_Should_Create_New_Basket_For_User()
        {
            var createBasketModel = new CreateBasketModel { BuyerId = Guid.NewGuid().ToString() };
            var response = await Client.PostAsJsonAsync("/api/basket/",createBasketModel);
            response.EnsureSuccessStatusCode();
            var createdBasketResponse = await Client.GetAsync("api/basket/"+createBasketModel.BuyerId);
            createdBasketResponse.EnsureSuccessStatusCode();
            //var basket = await createdBasketResponse.Content.ReadAsAsync<Basket>();
            //Assert.Equal(createBasketModel.BuyerId, basket.BuyerId);
        }

        [Fact]
        public async Task Add_Item_ToBasket_Should_Crete_Basket_And_Add()
        {
            var addBasketModel = new BasketITemPostModel
            {
                BasketId = Guid.NewGuid(),
                CatalogItemId = Guid.NewGuid(),
                Quantity = 5M,
                Price = 10.25M
            };
            var response = await Client.PutAsJsonAsync("/api/basket/" + addBasketModel.BasketId, addBasketModel);
        }

        public HttpClient Client { get; }
    }

    public class BasketITemPostModel
    {
        public Guid BasketId { get; set; }
        public Guid CatalogItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateBasketModel
    {
        public string BuyerId { get; set; }
    }

}
