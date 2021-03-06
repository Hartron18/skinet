using Microsoft.EntityFrameworkCore;
using SkinetApi.Contracts;
using SkinetApi.Entities;
using SkinetApi.Specification;

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

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        }
    }
}
