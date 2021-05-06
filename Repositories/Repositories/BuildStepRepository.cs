using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class BuildStepRepository : AbstractRepository<BuildStep>
    {
        public BuildStepRepository(DatabaseContext dbContext) : base(dbContext, dbContext.BuildSteps)
        {
        }

        public async Task<List<BuildStep>> GetAllFromBuildConfigAsync(Guid buildConfigId)
        {
            var allBuildSteps = await DbSet.ToListAsync();
            return allBuildSteps.Where(x => x.BuildConfigId == buildConfigId).ToList();
        }
    }
}
