using Microsoft.AspNetCore.Mvc;
using SkinetApi.Entities;

namespace SkinetApi.Contracts
{
    public interface IGeneralRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int Id);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetTypesAsync();
    }
}
