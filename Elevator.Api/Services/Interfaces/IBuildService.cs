using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Elevator.Api.Services.Interfaces
{
    public interface IBuildService
    {
        Task<List<Build>> GetAllBuildsAfterDateAsync(Guid buildConfigId, DateTime dateTime);
        Task<Build> GetLastBuildAsync(Guid buildConfigId);
        Task<Build> GetByIdAsync(Guid buildConfigId, Guid buildId);
    }
}
