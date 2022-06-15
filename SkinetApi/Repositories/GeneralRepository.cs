using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinetApi.Contracts;
using SkinetApi.Entities;

namespace SkinetApi.Repositories
{
    public class GeneralRepository : IGeneralRepository
    { 
        public StoreDbContext _context { get; }
        public GeneralRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType).SingleOrDefaultAsync(p => p.ProductTypeId == Id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
        {
           return await _context.ProductTypes.ToListAsync();
        }
    }
}
