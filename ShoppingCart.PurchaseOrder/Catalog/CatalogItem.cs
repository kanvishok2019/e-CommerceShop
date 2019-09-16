using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.Catalog
{
    public class CatalogItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public CatalogType CatalogType { get; set; }

    }
}
