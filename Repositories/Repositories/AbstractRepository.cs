using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;

namespace Repositories.Repositories
{
    public class AbstractRepository<T> 
        where T : class
    {
        protected readonly DatabaseContext DbContext;
        protected readonly DbSet<T> DbSet;

        protected AbstractRepository(DatabaseContext dbContext, DbSet<T> dbSet)
        {
            DbContext = dbContext;
            DbSet = dbSet;
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

        public virtual Task<List<T>> GetAllAsync()
        {
            return DbSet.ToListAsync();
        }
    }
}
