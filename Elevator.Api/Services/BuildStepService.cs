﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Models;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class BuildStepService : IBuildStepService
    {
        private readonly BuildStepRepository buildStepRepository;

        public BuildStepService(BuildStepRepository buildStepRepository)
        {
            this.buildStepRepository = buildStepRepository;
        }

        public async Task<List<BuildStep>> GetAllFromBuildConfigAsync(Guid buildConfigId)
        {
            var buildSteps = await buildStepRepository.GetAllFromBuildConfigAsync(buildConfigId);
            return buildSteps
                .Select(ModelsMapper.ConvertBuildStepDbModelToServiceModel)
                .ToList();
        }

        public async Task<BuildStep> CreateAsync(BuildStep buildStep)
        {
            var dbBuildStep = await buildStepRepository.AddAsync(ModelsMapper.ConvertBuildStepServiceModelToDbModel(buildStep));
            return ModelsMapper.ConvertBuildStepDbModelToServiceModel(dbBuildStep);
        }
    }
}
