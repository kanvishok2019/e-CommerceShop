using System.Threading.Tasks;
using Infrastructure.Core.Query;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Query;
using ShoppingCart.ApplicationCore.Basket.Query.Specifications;

namespace ShoppingCart.ApplicationCore.Basket.Handlers.QueryHandlers
{
    public class BasketQueryHandlers : IQueryHandler<GetBasketByBuyerId, Query.ViewModel.Basket>
    {
        private readonly IAsyncRepository<Query.ViewModel.Basket> _basketVieModelRepository;

        public BasketQueryHandlers(IUnitOfWork unitOfWork)
        {
            _basketVieModelRepository = unitOfWork.GetRepositoryAsync<Query.ViewModel.Basket>();
        }

        public async Task<Query.ViewModel.Basket> HandleAsync(GetBasketByBuyerId query)
        {
            var basketWithItemSpecification = new BasketWithItemsSpecification(query.BuyerId);
            return await _basketVieModelRepository.FirstOrDefaultAsync(basketWithItemSpecification);
        }
    }
}
