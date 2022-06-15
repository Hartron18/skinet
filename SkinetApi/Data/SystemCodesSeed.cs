using Microsoft.EntityFrameworkCore;
using SkinetApi.Entities;
using SkinetApi.Repositories;
using System.Text.Json;

namespace SkinetApi.Data
{
    public class SystemCodesSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    await context.Database.ExecuteSqlRawAsync(@$"SET IDENTITY_INSERT SystemCodes ON;");

                    if (!context.SystemCodes.Any())
                    {
                        var systemCodesData = File.ReadAllText("../skinetapi/Data/SeedData/SystemCodesData.json");

                        var systemCodes = JsonSerializer.Deserialize<List<SystemCodes>>(systemCodesData);

                        foreach (var systemCode in systemCodes)
                        {
                            //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[SystemCodes] ON;");
                            context.SystemCodes.Add(systemCode);
                        }
                    }
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlInterpolatedAsync(@$"SET IDENTITY_INSERT SystemCodes OFF;");
                    
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<SystemCodesSeed>();
                logger.LogError(ex.Message);
            }
            
        }
    }
}
