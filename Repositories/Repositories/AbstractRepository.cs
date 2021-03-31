using System.Threading.Tasks;
using Repositories.Database;

namespace Repositories.Repositories
{
    public class AbstractRepository<T> 
        where T : class
    {
        protected readonly DatabaseContext DbContext;

        protected AbstractRepository(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await DbContext.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> FindByIdAsync(string id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public virtual async Task RemoveAsync(T entity)
        {
            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            DbContext.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

    }
}
