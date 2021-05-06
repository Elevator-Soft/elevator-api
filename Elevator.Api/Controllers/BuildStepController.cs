using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Dto;
using Elevator.Api.Extensions.Dto;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Elevator.Api.Controllers
{
    [ApiController]
    public class BuildStepController : ControllerBase
    {
        private readonly ILogger<BuildStepController> logger;
        private readonly IBuildStepService buildStepService;

        public BuildStepController(ILogger<BuildStepController> logger, IBuildStepService buildStepService)
        {
            this.logger = logger;
            this.buildStepService = buildStepService;
        }

        [HttpPost("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/buildSteps")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<OperationResult<BuildStepDto>> CreateAsync([FromBody] CreateBuildStepRequestDto createBuildStepRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(CreateAsync)}'");
            logger.LogInformation($"CreateProjectRequestDto: '{createBuildStepRequestDto}'");

            var buildStep =
                await buildStepService.CreateAsync(createBuildStepRequestDto.ToServiceModel());

            var dtoModel = ModelsMapper.CovertBuildStepServiceModelToDto(buildStep);
            return OperationResult<BuildStepDto>.Created(dtoModel);
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/buildSteps")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<OperationResult<List<BuildStepDto>>> GetAllAsync([FromRoute] Guid buildConfigId)
        {
            logger.LogInformation($"Start execution method '{nameof(GetAllAsync)}'");

            var buildSteps = await buildStepService.GetAllFromBuildConfigAsync(buildConfigId);
            var dtoModels = buildSteps.Select(ModelsMapper.CovertBuildStepServiceModelToDto).ToList();

            return OperationResult<List<BuildStepDto>>.Ok(dtoModels);
        }
    }
}
