using System;
using System.Threading.Tasks;
using Infrastructure.Core.Event;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.PurchaseOrder.Events;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.ViewModelGenerators
{
    public class ShippingInvoiceViewModelGenerator : IEventHandler<ProductPurchasedEvent>
    {
        private readonly IAsyncRepository<ShippingInvoice> _shippingInvoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShippingInvoiceViewModelGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _shippingInvoiceRepository = _unitOfWork.GetRepositoryAsync<ShippingInvoice>();
        }

        public async Task HandleAsync(ProductPurchasedEvent @event)
        {
            var shippingInvoice = new ShippingInvoice(@event.PurchaseOrderNo, @event.BuyerId, @event.PurchaseOrderList, @event.ShippingAddress);
            await _shippingInvoiceRepository.AddAsync(shippingInvoice);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
