using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.GuardClauses;
using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public class CatalogItemOrdered : BaseEntity
    {
        public CatalogItemOrdered(int catalogItemId, CatalogItemType catalogItemType,
            string productName, string pictureUri)
        {
            Guard.Against.OutOfRange(catalogItemId, nameof(catalogItemId), 1, int.MaxValue);
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));
            Guard.Against.Null(catalogItemType, nameof(catalogItemType));
            CatalogItemId = catalogItemId;
            CatalogItemType = catalogItemType;
            ProductName = productName;
            PictureUri = pictureUri;
        }

        public int CatalogItemId { get; private set; }
        public CatalogItemType CatalogItemType { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
    }
}
