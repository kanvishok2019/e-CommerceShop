using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Query;
using Infrastructure.Core.Repository;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.Specifications;
using ShoppingCart.ApplicationCore.PurchaseOrder.Query.ViewModel;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Handlers.QueryHandler
{
    public class GetShippingInvoiceQueryHandler : IQueryHandler<ShippingInvoiceQuery, ShippingInvoice>
    {
        private readonly IAsyncRepository<ShippingInvoice> _shippingInvoiceAsyncRepository;

        public GetShippingInvoiceQueryHandler(IUnitOfWork unitOfWork)
        {
            _shippingInvoiceAsyncRepository = unitOfWork.GetRepositoryAsync<ShippingInvoice>();
        }

        public async Task<ShippingInvoice> HandleAsync(ShippingInvoiceQuery query)
        {
            var shippingInvoiceSpecification = new ShippingInvoiceSpecification(query.PurchaseOrderNo);
            return await _shippingInvoiceAsyncRepository.FirstOrDefaultAsync(shippingInvoiceSpecification);
        }
    }
}
