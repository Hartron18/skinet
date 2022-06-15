using Microsoft.EntityFrameworkCore;
using SkinetApi.Entities;
using System.Text.Json;

namespace SkinetApi.Data
{
    public class Seed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Database.ExecuteSqlInterpolated(@$"SET IDENTITY_INSERT ProductBrands ON;");
                    if (!context.ProductBrands.Any())
                    {
                        var path = Directory.GetCurrentDirectory();
                        var brands = File.ReadAllText(path + @"\Data\SeedData\brands.json");
                        var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brands);

                        foreach (var brand in productBrands)
                        {
                            await context.AddAsync(brand);
                        }
                    }

                    await context.SaveChangesAsync();

                    context.Database.ExecuteSqlInterpolated(@$"SET IDENTITY_INSERT ProductBrands OFF;");
                    transaction.Commit();
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    await context.Database.ExecuteSqlInterpolatedAsync(@$"SET IDENTITY_INSERT ProductTypes ON;");
                    if (!context.ProductTypes.Any())
                    {
                        var path = Directory.GetCurrentDirectory();
                        var types = File.ReadAllText(path + @"\Data\SeedData\types.json");
                        var productTypes = JsonSerializer.Deserialize<List<ProductType>>(types);

                        foreach (var type in productTypes)
                        {
                            await context.AddAsync(type);
                        }
                    }

                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlInterpolatedAsync(@$"SET IDENTITY_INSERT ProductTypes OFF;");
                    transaction.Commit();
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    if(!context.Products.Any())
                    {
                        var path = Directory.GetCurrentDirectory();
                        var products = File.ReadAllText(path + @"\Data\SeedData\products.json");
                        var productsData = JsonSerializer.Deserialize<List<Product>>(products);

                        foreach (var product in productsData)
                        {
                            await context.AddAsync(product);
                        }
                    }
                 
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
               var logger = loggerFactory.CreateLogger<Seed>();
                logger.LogError(ex.Message);
            }
            

            return;
        }
    }
}
