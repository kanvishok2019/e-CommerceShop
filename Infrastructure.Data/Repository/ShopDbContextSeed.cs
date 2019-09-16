using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ShoppingCart.ApplicationCore.Catalog;

namespace Infrastructure.Data.Repository
{
    public class ShopDbContextSeed
    {
        public static async Task SeedAsync(ShopDbContext shopContext)
        {
            try
            {
                if (!shopContext.CatalogItems.Any())
                {
                    shopContext.CatalogItems.AddRange(
                        GetPreconfiguredItems());

                    await shopContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem() {Id  = 1, Name = "Pro C# 7: With .NET and .NET Core - Book",CatalogType = CatalogType.Product,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 2, Name = "Pro C# 7: With .NET and .NET Core - Video",CatalogType = CatalogType.Product,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 3, Name = "Book Club Membership",CatalogType = CatalogType.Subscription,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 4, Name = "Video Club Membership",CatalogType = CatalogType.Subscription,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 5, Name = "Premium Club Membership",CatalogType = CatalogType.Subscription,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
            };
        }
    }
}
