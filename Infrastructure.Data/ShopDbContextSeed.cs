using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.ApplicationCore.Buyer;
using ShoppingCart.ApplicationCore.Catalog;

namespace Infrastructure.Data
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
                if (!shopContext.Buyers.Any())
                {
                    shopContext.Buyers.AddRange(
                        GetPreconfiguredBuyers());
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
                new CatalogItem() {Id  = 1, Name = "Book Club Membership",CatalogType = CatalogType.Subscription,Description = "Book Club Membership",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 2, Name = "Video Club Membership",CatalogType = CatalogType.Subscription,Description = "Video Club Membership",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 3, Name = "Pro C# 7: With .NET and .NET Core - Book",CatalogType = CatalogType.Product,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
                new CatalogItem() {Id  = 4, Name = "Pro C# 7: With .NET and .NET Core - Video",CatalogType = CatalogType.Product,Description = "Pro C# 7: With .NET and .NET Core",PictureUri = "https://images-na.ssl-images-amazon.com/images/I/413Z89zzezL._SX348_BO1,204,203,200_.jpg",Price = 10.50M},
            };
        }

        static IEnumerable<Buyer> GetPreconfiguredBuyers()
        {
            return new List<Buyer>()
            {
                new Buyer() { Name= "Test User1", SubscriptionPlan  = null},
                new Buyer() { Name= "Test User2", SubscriptionPlan  = null},
                new Buyer() { Name= "Test User3", SubscriptionPlan  = null},
                new Buyer() { Name= "Test User4", SubscriptionPlan  = null},
                new Buyer() { Name= "Test User5", SubscriptionPlan  = SubscriptionPlan.BookClubSubscription},
                new Buyer() { Name= "Test User6", SubscriptionPlan  = SubscriptionPlan.VideoClubSubscription},
                new Buyer() { Name= "Test User7", SubscriptionPlan  = SubscriptionPlan.PremiumClubSubscription},
            };
        }
    }
}
