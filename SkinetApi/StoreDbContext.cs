using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkinetApi.Entities;
using SkinetApi.Entities.Identity;
using System.Reflection;

namespace SkinetApi
{
    public class StoreDbContext: IdentityDbContext<AppUser, Role, string>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var product1 = new Product { Id = 1, Name = "Product one" };
            //var product2 = new Product { Id = 2, Name = "Product two" };
            //var product3 = new Product { Id = 3, Name = "Product three" };

            //modelBuilder.Entity<Product>().HasData(product1, product2, product3);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //IdentityRolesSeeding.SeedRoleAsync();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<SystemCodes> SystemCodes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        
       
    }
}
