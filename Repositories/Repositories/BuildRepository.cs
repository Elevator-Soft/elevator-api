using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class BuildRepository : AbstractRepository<Build>
    {
        public BuildRepository(DatabaseContext dbContext) : base(dbContext, dbContext.Builds)
        {
        }

        public async Task<List<Build>> GetAllFromBuildConfigAsync(Guid buildConfigId)
        {
            var sql = "select * from \"Builds\" \n" + $"where \"BuildConfigId\"='{buildConfigId}'";
            return await DbSet.FromSqlRaw(sql).ToListAsync();
        }
    }
}
