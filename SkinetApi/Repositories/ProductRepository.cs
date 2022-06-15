using Microsoft.EntityFrameworkCore;
using SkinetApi.Contracts;
using SkinetApi.Entities;

namespace SkinetApi.Repositories
{
    public class ProductRepository: RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProducts(bool trackChanges) =>
            await FindAll(trackChanges).Include(p => p.ProductBrand)
            .Include(p => p.ProductType).ToListAsync();

        public async Task<Product> GetProduct(int id, bool trackChanges) =>
            await FindByCondition(x => x.Id == id, trackChanges).FirstOrDefaultAsync();

        public void CreateProduct(Product product) => CreateProduct(product);

    }
}
