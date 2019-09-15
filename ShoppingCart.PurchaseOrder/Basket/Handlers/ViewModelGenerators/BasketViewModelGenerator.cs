using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Event;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Events;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.ViewModelGenerators
{
    public class BasketViewModelGenerator:IEventHandler<BasketCreatedEvent>
    {
        private readonly IAsyncRepository<Query.ViewModel.Basket> _basketAsyncRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BasketViewModelGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _basketAsyncRepository = _unitOfWork.GetRepositoryAsync<Query.ViewModel.Basket>();
        }

        public async Task HandleAsync(BasketCreatedEvent @event)
        {
            await _basketAsyncRepository.AddAsync(new Query.ViewModel.Basket
            {
                BasketId = @event.BasketId,
                BuyerId = @event.BuyerId
            });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
