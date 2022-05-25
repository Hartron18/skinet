using Microsoft.EntityFrameworkCore;
using SkinetApi.Entities;

namespace SkinetApi
{
    public class StoreDbContext: DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var product1 = new Product { Id = 1, Name = "Product one" };
            var product2 = new Product { Id = 2, Name = "Product two" };
            var product3 = new Product { Id = 3, Name = "Product three" };

            modelBuilder.Entity<Product>().HasData(product1, product2, product3);
        }

        public DbSet<Product> Products { get; set; }
    }
}
