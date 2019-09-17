using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Event;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Events;
using ShoppingCart.ApplicationCore.Basket.Query.Specifications;
using ShoppingCart.ApplicationCore.Basket.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.ViewModelGenerators
{
    public class BasketItemAddedViewModelGenerator : IEventHandler<ItemAddedToBasketEvent>
    {
        private readonly IAsyncRepository<Query.ViewModel.Basket> _basketItemAsyncRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        public BasketItemAddedViewModelGenerator(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _basketItemAsyncRepository = _unitOfWork.GetRepositoryAsync<Query.ViewModel.Basket>();
        }

        public async Task HandleAsync(ItemAddedToBasketEvent @event)
        {
            try
            {
                var basketItem = _autoMapper.Map<Domain.BasketItem, BasketItem>(@event.BasketItem);
                var basketSpecification = new BasketWithItemsSpecification(@event.BasketId);
                var basket = await _basketItemAsyncRepository.GetSingleAsync(basketSpecification);
                basket.BasketItems.Add(basketItem);
                _basketItemAsyncRepository.Update(basket);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
