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
    public class BuildConfigService : IBuildConfigService
    {
        private readonly BuildConfigRepository buildConfigRepository;

        public BuildConfigService(BuildConfigRepository buildConfigRepository)
        {
            this.buildConfigRepository = buildConfigRepository;
        }

        public async Task<List<BuildConfig>> GetAllFromProjectAsync(Guid projectId)
        {
            var allBuildConfigs = await buildConfigRepository.GetAllFromProjectAsync(projectId);
            return allBuildConfigs
                .Select(ModelsMapper.ConvertBuildConfigDbModelToServiceModel)
                .ToList();
        }

        public async Task<BuildConfig> CreateAsync(BuildConfig buildConfig)
        {
            var dbBuildConfig =
                await buildConfigRepository.AddAsync(ModelsMapper.ConvertBuildConfigServiceModelToDbModel(buildConfig));
            return ModelsMapper.ConvertBuildConfigDbModelToServiceModel(dbBuildConfig);
        }
    }
}
