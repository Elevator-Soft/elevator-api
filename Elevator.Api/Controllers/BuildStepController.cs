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
        public async Task<HttpOperationResult<BuildStepDto>> CreateAsync([FromBody] CreateBuildStepRequestDto createBuildStepRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(CreateAsync)}'");
            logger.LogInformation($"CreateProjectRequestDto: '{createBuildStepRequestDto}'");

            var buildStep =
                await buildStepService.CreateAsync(createBuildStepRequestDto.ToServiceModel());

            var dtoModel = ModelsMapper.CovertBuildStepServiceModelToDto(buildStep);
            return HttpOperationResult<BuildStepDto>.Created(dtoModel);
        }

        [HttpPut("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/buildSteps/{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpVoidOperationResult> UpdateAsync([FromBody] UpdateBuildStepRequestDto updateBuildStepRequestDto, [FromRoute] Guid id)
        {
            logger.LogInformation($"Start execution method '{nameof(UpdateAsync)}'");
            logger.LogInformation($"UpdateBuildStepRequestDto: '{updateBuildStepRequestDto}'");

            await buildStepService.UpdateAsync(id, updateBuildStepRequestDto.ToServiceModel());

            return HttpVoidOperationResult.Ok();
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/buildSteps")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<List<BuildStepDto>>> GetAllAsync([FromRoute] Guid buildConfigId)
        {
            logger.LogInformation($"Start execution method '{nameof(GetAllAsync)}'");

            var buildSteps = await buildStepService.GetAllFromBuildConfigAsync(buildConfigId);
            var dtoModels = buildSteps.Select(ModelsMapper.CovertBuildStepServiceModelToDto).ToList();

            return HttpOperationResult<List<BuildStepDto>>.Ok(dtoModels);
        }


    }
}
