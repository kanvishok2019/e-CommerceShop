using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingCart.Api.Models;
using ShoppingCart.ApplicationCore.Basket.Commands;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;

namespace ShoppingCart.Api.Configurators
{
    public class AutoMappingConfiguration : Profile
    {
        public AutoMappingConfiguration()
        {
            ShouldMapField = fieldInfo => true;
            ShouldMapProperty = propertyInfo => true;
            CreateMap<BasketItemModel, AddItemToBasketCommand>();
            CreateMap<CreateBasketModel, CreateBasketForUserCommand>();
            CreateMap<ShoppingCart.ApplicationCore.Basket.Domain.Basket, Basket>();
            CreateMap<Buyer, Models.Buyer>();



        }
    }
}
