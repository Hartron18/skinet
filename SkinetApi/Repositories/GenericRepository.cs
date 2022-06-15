using Microsoft.EntityFrameworkCore;
using SkinetApi.Contracts;
using SkinetApi.Entities;

namespace SkinetApi.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T: BaseEntity
    {
        private readonly StoreDbContext context;

        public GenericRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync(); 
        }

    }
}
