using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Elevator.Api.Services.Interfaces
{
    public interface IBuildStepService
    {
        Task<List<BuildStep>> GetAllFromBuildConfigAsync(Guid buildConfigId);
        Task<BuildStep> CreateAsync(BuildStep buildStep);
    }
}
