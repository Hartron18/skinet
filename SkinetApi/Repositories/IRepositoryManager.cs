using SkinetApi.Contracts;

namespace SkinetApi.Repositories
{
    public interface IRepositoryManager
    {
        IProductRepository ProductRepository { get; }
        ISystemCodesRepository SystemCodesRepository { get; }

        Task SaveAsync();
    }
}
