using Microsoft.EntityFrameworkCore;
using SkinetApi.Contracts;
using System.Linq.Expressions;

namespace SkinetApi.Repositories
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T : class 
    {
        private readonly StoreDbContext context;

        public RepositoryBase(StoreDbContext context)
        {
            this.context = context;
        }
        public IQueryable<T> FindAll(bool trackChanges) => 
            ! trackChanges? 
            context.Set<T>().AsNoTracking() : context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges ) =>
            !trackChanges? 
            context.Set<T>().Where(expression).AsNoTracking() : context.Set<T>().Where(expression);

        public void Create(T entity) => context.Set<T>().Add(entity);
        public void Update(T entity) => context.Set<T>().Update(entity);
        public void Delete(T entity) => context.Set<T>().Remove(entity);
        
            
    }
}
