using System.Text.Json;
using Talabat.Domain.order;
using Talabat.Domain.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;

namespace Talabat.Infrastructure.Common.DataSeed
{
    public static class SeedTalabatContext
    {
        public static async Task SeedDataAsync(TalabatDbContext talabatDbContext)
        {
            if (!talabatDbContext.ProductBrands.Any()) 
            {
                var BrandsData = File.ReadAllText("../Talabat.Infrastrucure/Common/DataSeed/Files/brands.json");
                var Brands = JsonSerializer.Deserialize<ICollection<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await talabatDbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await talabatDbContext.SaveChangesAsync();
                }
            }

            if (!talabatDbContext.ProductTypes.Any())
            {
                var productTypes = File.ReadAllText("../Talabat.Infrastrucure/Common/DataSeed/Files/types.json");
                var Types = JsonSerializer.Deserialize<ICollection<ProductType>>(productTypes);
                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await talabatDbContext.Set<ProductType>().AddAsync(type);
                    }
                    await talabatDbContext.SaveChangesAsync();
                }
            }

            if (!talabatDbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Infrastrucure/Common/DataSeed/Files/products.json");
                var products = JsonSerializer.Deserialize<ICollection<Product>>(productsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await talabatDbContext.Set<Product>().AddAsync(product);
                    }
                    await talabatDbContext.SaveChangesAsync();
                }
            }

            if (!talabatDbContext.DeliveryMethods.Any())
            {
                var DeleveryMethodsData = File.ReadAllText("../Talabat.Infrastrucure/Common/DataSeed/Files/delivery.json");
                var DeleveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeleveryMethodsData);
                if (DeleveryMethods?.Count > 0)
                {
                    foreach (var DeleveryMethod in DeleveryMethods)
                    {
                        await talabatDbContext.Set<DeliveryMethod>().AddAsync(DeleveryMethod);
                    }
                }
                await talabatDbContext.SaveChangesAsync();
            }
        }
    }
}
