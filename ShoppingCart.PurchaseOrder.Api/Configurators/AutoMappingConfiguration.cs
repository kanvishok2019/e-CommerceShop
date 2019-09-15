using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingCart.Api.Models;
using ShoppingCart.ApplicationCore.Basket.Commands;

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
        }
    }
}
