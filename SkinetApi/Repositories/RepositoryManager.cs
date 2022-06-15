using SkinetApi.Contracts;

namespace SkinetApi.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly StoreDbContext context;

        public RepositoryManager(StoreDbContext context)
        {
            this.context = context;
        }

        private IProductRepository _ProductRepository;
        private ISystemCodesRepository _SystemCodesRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                if (_ProductRepository == null)
                    _ProductRepository = new ProductRepository(context);

                return _ProductRepository;
            }
        }

        public ISystemCodesRepository SystemCodesRepository
        {
            get
            {
                if (_SystemCodesRepository is null)
                    _SystemCodesRepository = new SystemCodesRepository(context);

                return _SystemCodesRepository;
            }
        }

        public Task SaveAsync() => context.SaveChangesAsync();


    }
}
