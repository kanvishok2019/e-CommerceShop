using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.CommandHandlers
{
    public class ProcessPurchaseOrderCommandHandler : ICommandHandler<ProcessPurchaseOrderCommand>
    {
        private readonly IAggregateRepositoryService<Domain.PurchaseOrder> _shopAggregateRepositoryService;
        private readonly IAsyncRepository<PurchaseOrderIdNumberMapping> _purchaseOrderIdNumberMappingAsyncRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProcessPurchaseOrderCommandHandler(IUnitOfWork unitOfWork, IAggregateRepositoryService<Domain.PurchaseOrder> shopAggregateRepositoryService)
        {
            _unitOfWork = unitOfWork;
            _shopAggregateRepositoryService = shopAggregateRepositoryService;
            _purchaseOrderIdNumberMappingAsyncRepository =
                _unitOfWork.GetRepositoryAsync<PurchaseOrderIdNumberMapping>();
        }

        public async Task HandleAsync(ProcessPurchaseOrderCommand command)
        {
            var purchaseOrderMapping =
                await _purchaseOrderIdNumberMappingAsyncRepository.GetByIdAsync(command.PurchaseOrderNo);
            var purchaseOrder = await _shopAggregateRepositoryService.GetAsync(purchaseOrderMapping.PurchaseOrderId);
            if (!purchaseOrder.IsPurchaseOrderProcessed && purchaseOrder.OrderItems.Count > 0)
            {
                await purchaseOrder.ProcessPurchaseOrder();
                await _shopAggregateRepositoryService.SaveAsync(purchaseOrder);
            }

        }
    }
}
