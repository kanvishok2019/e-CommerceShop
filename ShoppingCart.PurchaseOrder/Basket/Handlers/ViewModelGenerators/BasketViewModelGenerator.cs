using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Event;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Events;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.ViewModelGenerators
{
    public class BasketViewModelGenerator : IEventHandler<BasketCreatedEvent>
    {
        private readonly IAsyncRepository<Query.ViewModel.Basket> _basketAsyncRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        public BasketViewModelGenerator(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _basketAsyncRepository = _unitOfWork.GetRepositoryAsync<Query.ViewModel.Basket>();
        }

        public async Task HandleAsync(BasketCreatedEvent @event)
        {
            _basketAsyncRepository.Add(new Query.ViewModel.Basket
            {
                BasketId = @event.BasketId,
                BuyerId = @event.BuyerId
            });
            await _unitOfWork.SaveChangesAsync();

        }


    }
}
