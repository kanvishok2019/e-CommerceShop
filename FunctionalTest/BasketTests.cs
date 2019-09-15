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
        private readonly CreateBasketModel _basketModel;

        public BasketTests(ShopWebApplicationFactory<Startup> factory)
        {
            _basketModel = new CreateBasketModel { BuyerId = Guid.NewGuid().ToString() };
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Create_New_Basket_Should_Create_New_Basket_For_User()
        {

            var response = await Client.PostAsJsonAsync("/api/basket/", _basketModel);
            response.EnsureSuccessStatusCode();
            var createdBasketResponse = await Client.GetAsync("api/basket/" + _basketModel.BuyerId);
            createdBasketResponse.EnsureSuccessStatusCode();
            var basket = await createdBasketResponse.Content.ReadAsAsync<Basket>();
            Assert.Equal(_basketModel.BuyerId, basket.BuyerId);
        }

        [Fact]
        public async Task Add_Item_ToBasket_Should_Crete_Basket_And_Add()
        {
            var response = await Client.PostAsJsonAsync("/api/basket/", _basketModel);
            response.EnsureSuccessStatusCode();
            var createdBasketResponse = await Client.GetAsync("api/basket/" + _basketModel.BuyerId);
            createdBasketResponse.EnsureSuccessStatusCode();
            var basket = await createdBasketResponse.Content.ReadAsAsync<Basket>();
            var addBasketModel = new BasketITemPostModel
            {
                BasketId = basket.BasketId,
                CatalogItemId = 1,
                Quantity = 5M,
                Price = 10.25M
            };
            var itemAddedResponse = await Client.PutAsJsonAsync("/api/basket/" + addBasketModel.BasketId, addBasketModel);
            itemAddedResponse.EnsureSuccessStatusCode();
            var afterItemAddedResponse = await Client.GetAsync("api/basket/" + _basketModel.BuyerId);
            afterItemAddedResponse.EnsureSuccessStatusCode();
            basket = await afterItemAddedResponse.Content.ReadAsAsync<Basket>();
        }

        public HttpClient Client { get; }
    }

    public class BasketITemPostModel
    {
        public Guid BasketId { get; set; }
        public int CatalogItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateBasketModel
    {
        public string BuyerId { get; set; }
    }

}