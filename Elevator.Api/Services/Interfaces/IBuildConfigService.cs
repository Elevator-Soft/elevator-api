using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Elevator.Api.Services.Interfaces
{
    public interface IBuildConfigService
    {
        Task<List<BuildConfig>> GetAllFromProjectAsync(Guid projectId);
        Task<BuildConfig> CreateInProjectAsync(BuildConfig buildConfig);
    }
}
