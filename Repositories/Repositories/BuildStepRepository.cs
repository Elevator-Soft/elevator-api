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
            var sql = "select * from \"BuildSteps\" \n" + $"where \"BuildConfigId\"='{buildConfigId}'";
            var buildSteps = await DbSet.FromSqlRaw(sql).ToListAsync();
            return buildSteps.Select(x =>
            {
                x.BuildStepScript = DbContext.Find<BuildStepScript>(x.BuildStepScriptId);
                return x;
            }).ToList();
        }

        public override async Task<BuildStep> FindByIdAsync(Guid id)
        {
            var step = await base.FindByIdAsync(id);
            var script = DbContext.Find<BuildStepScript>(step.BuildStepScriptId);
            step.BuildStepScript = script;
            return step;
        }
    }
}
