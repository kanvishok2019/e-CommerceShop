﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Event;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Events;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.ViewModelGenerators
{
    public class BasketItemAddedViewModelGenerator : IEventHandler<ItemAddedToBasketEvent>
    {
        private readonly IAsyncRepository<Query.ViewModel.BasketItem> _basketItemAsyncRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        public BasketItemAddedViewModelGenerator(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _basketItemAsyncRepository = _unitOfWork.GetRepositoryAsync<Query.ViewModel.BasketItem>();
        }

        public async Task HandleAsync(ItemAddedToBasketEvent @event)
        {
            var basketItem = _autoMapper.Map<Domain.BasketItem, BasketItem>(@event.BasketItem);
            await _basketItemAsyncRepository.AddAsync(basketItem);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
