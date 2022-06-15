using SkinetApi.Entities;

namespace SkinetApi.Contracts
{
    public interface IProductRepository
    {
       
        void CreateProduct(Product product);
        Task<Product> GetProduct(int id, bool trackChanges);
        Task<IEnumerable<Product>> GetProducts(bool trackChanges);
    }
}
