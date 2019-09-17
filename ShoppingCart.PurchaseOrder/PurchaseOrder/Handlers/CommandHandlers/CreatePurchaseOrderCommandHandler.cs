using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Command;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.Basket.Query.Specifications;
using ShoppingCart.ApplicationCore.Catalog;
using ShoppingCart.ApplicationCore.PurchaseOrder.Commands;
using ShoppingCart.ApplicationCore.PurchaseOrder.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.CommandHandlers
{
    public class CreatePurchaseOrderCommandHandler:ICommandHandler<CreatePurchaseOrderCommand>
    {
        private readonly IAggregateRepositoryService<Domain.PurchaseOrder> _shopAggregateRepositoryService;
        private readonly IAsyncRepository<Basket.Query.ViewModel.Basket> _basketAsyncRepository;
        private readonly IAsyncRepository<PurchaseOrderIdNumberMapping> _purchaseOrderIdNumberMappingAsyncRepository;
        private readonly IAsyncRepository<CatalogItem> _catalogItemAsyncRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        public CreatePurchaseOrderCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper, IAggregateRepositoryService<Domain.PurchaseOrder> shopAggregateRepositoryService)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _shopAggregateRepositoryService = shopAggregateRepositoryService;
            _basketAsyncRepository = _unitOfWork.GetRepositoryAsync<Basket.Query.ViewModel.Basket>();
            _catalogItemAsyncRepository = _unitOfWork.GetRepositoryAsync<CatalogItem>();
            _purchaseOrderIdNumberMappingAsyncRepository =
                _unitOfWork.GetRepositoryAsync<PurchaseOrderIdNumberMapping>();


        }

        public async Task HandleAsync(CreatePurchaseOrderCommand command)
        {
            //Issue: Crossing the boundary context to cut shot the time.
            //This should call the Basket Api and get the details. Not the repository directly.
            var basketSpecification = new BasketWithItemsSpecification(command.BasketId);
            var basket = await _basketAsyncRepository.GetSingleAsync(basketSpecification);

            var items = new List<PurchaseOrderItem>();
            foreach (var basketItem in basket.BasketItems)
            {
                var catalogItem = await _catalogItemAsyncRepository.GetByIdAsync(basketItem.CatalogItemId);
                var itemOrdered = new CatalogItemOrdered(catalogItem.Id, 
                    (CatalogItemType)catalogItem.CatalogType, catalogItem.Name, catalogItem.PictureUri);
                var orderItem = new PurchaseOrderItem(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
                items.Add(orderItem);
            }

            var purchaseOrderId = Guid.NewGuid();
            var purchaseOrderIdNumberMapping = new PurchaseOrderIdNumberMapping{PurchaseOrderId = purchaseOrderId};
            await _purchaseOrderIdNumberMappingAsyncRepository.AddAsync(purchaseOrderIdNumberMapping);
            await _unitOfWork.SaveChangesAsync();

            var purchaseOrder = new Domain.PurchaseOrder(purchaseOrderId, purchaseOrderIdNumberMapping.Id,  basket.BuyerId, command.Address, items);
            await _shopAggregateRepositoryService.SaveAsync(purchaseOrder);
        }
    }
}
