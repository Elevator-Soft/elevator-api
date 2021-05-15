using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Models;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class BuildService : IBuildService
    {
        private readonly BuildRepository buildRepository;

        public BuildService(BuildRepository buildRepository)
        {
            this.buildRepository = buildRepository;
        }

        public async Task<List<Build>> GetAllBuildsAfterDateAsync(Guid buildConfigId, DateTime dateTime)
        {
            var allBuilds = await buildRepository.GetAllFromBuildConfigAsync(buildConfigId);
            return allBuilds
                .Where(x => x.FinishTime >= dateTime)
                .Select(ModelsMapper.ConvertBuildDbModelToServiceModel)
                .ToList();
        }

        public async Task<Build> GetLastBuildAsync(Guid buildConfigId)
        {
            var allBuilds = await buildRepository.GetAllFromBuildConfigAsync(buildConfigId);
            return ModelsMapper.ConvertBuildDbModelToServiceModel(allBuilds.OrderByDescending(x => x.FinishTime).FirstOrDefault());
        }

        public async Task<Build> GetByIdAsync(Guid buildConfigId, Guid buildId)
        {
            var builds = await buildRepository.GetAllFromBuildConfigAsync(buildConfigId);
            return ModelsMapper.ConvertBuildDbModelToServiceModel(builds.FirstOrDefault(x => x.Id == buildId));
        }
    }
}
