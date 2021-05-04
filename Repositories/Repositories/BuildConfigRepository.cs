using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class BuildConfigRepository : AbstractRepository<BuildConfig>
    {
        protected BuildConfigRepository(DatabaseContext dbContext, DbSet<BuildConfig> dbSet) : base(dbContext, dbContext.BuildConfigs)
        {
        }

        public async Task<List<BuildConfig>> GetAllFromProjectAsync(Guid projectId)
        {
            var allBuildConfigs = await DbSet.ToListAsync();
            return allBuildConfigs.Where(x => x.ProjectId == projectId).ToList();
        }
    }
}
