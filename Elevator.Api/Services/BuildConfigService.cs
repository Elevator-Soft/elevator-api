using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Agent.Manager.Client.Models;
using Elevator.Agent.Manager.Client.Providers;
using Elevator.Api.Exceptions;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Models;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class BuildConfigService : IBuildConfigService
    {
        private readonly BuildConfigRepository buildConfigRepository;
        private readonly BuildTaskProvider buildTaskProvider;
        private readonly BuildRepository buildRepository;

        public BuildConfigService(BuildConfigRepository buildConfigRepository, BuildTaskProvider buildTaskProvider, BuildRepository buildRepository)
        {
            this.buildConfigRepository = buildConfigRepository;
            this.buildTaskProvider = buildTaskProvider;
            this.buildRepository = buildRepository;
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

        public async Task<Build> RunAsync(Guid id, string userId)
        {
            var dbBuildConfig = await buildConfigRepository.FindByIdAsync(id);
            var buildConfig = ModelsMapper.ConvertBuildConfigDbModelToServiceModel(dbBuildConfig);
            var buildTaskDto = new BuildTaskDto
            {
                BuildConfig = buildConfig,
                StartedByUserId = userId
            };
            var pushTaskResult = await buildTaskProvider.PushTaskAsync(buildTaskDto);
            if (!pushTaskResult.IsSuccessful)
                throw new Exception(pushTaskResult.Error);
            var build = await buildRepository.FindByIdAsync(pushTaskResult.Value.Id);
            if (build == null)
                throw new EntityNotFoundException(nameof(Build), pushTaskResult.Value.Id.ToString());
            return ModelsMapper.ConvertBuildDbModelToServiceModel(build);
        }
    }
}
