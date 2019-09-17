using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using Infrastructure.Core.Query;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Buyer.Commands;
using ShoppingCart.ApplicationCore.Buyer.Query;

namespace ShoppingCart.ApplicationCore.Buyer.Handlers
{
    public class GetAllBuyersQueryHandler : IQueryHandler<GetAllBuyers, IReadOnlyList<Buyer>>
    {
        private readonly IAsyncRepository<Buyer> _buyerAsyncRepository;

        public GetAllBuyersQueryHandler(IUnitOfWork unitOfWork)
        {
            _buyerAsyncRepository = unitOfWork.GetRepositoryAsync<Buyer>();
        }

        public async Task<IReadOnlyList<Buyer>> HandleAsync(GetAllBuyers query)
        {
            return await _buyerAsyncRepository.ListAllAsync();
        }
    }
}
