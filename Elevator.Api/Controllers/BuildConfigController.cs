using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Dto;
using Elevator.Api.Extensions.Dto;
using Elevator.Api.Services;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Elevator.Api.Controllers
{
    [ApiController]
    public class BuildConfigController : ControllerBase
    {
        private readonly ILogger<BuildConfigController> logger;
        private readonly BuildConfigService buildConfigService;

        public BuildConfigController(ILogger<BuildConfigController> logger, BuildConfigService buildConfigService)
        {
            this.logger = logger;
            this.buildConfigService = buildConfigService;
        }

        [HttpPost("projects/{projectId:guid}/buildConfigs")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<OperationResult<BuildConfigDto>> CreateAsync([FromBody] CreateBuildConfigRequestDto createBuildConfigRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(CreateAsync)}'");
            logger.LogInformation($"CreateProjectRequestDto: '{createBuildConfigRequestDto}'");

            var buildConfig =
                await buildConfigService.CreateAsync(createBuildConfigRequestDto.ToServiceModel());

            var dtoModel = ModelsMapper.ConvertServiceBuildConfigModelToDto(buildConfig);
            return OperationResult<BuildConfigDto>.Created(dtoModel);
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<OperationResult<List<BuildConfigDto>>> GetAllAsync([FromRoute]Guid projectId)
        {
            logger.LogInformation($"Start execution method '{nameof(GetAllAsync)}'");

            var buildConfigs = await buildConfigService.GetAllFromProjectAsync(projectId);
            var dtoModels = buildConfigs.Select(ModelsMapper.ConvertServiceBuildConfigModelToDto).ToList();

            return OperationResult<List<BuildConfigDto>>.Ok(dtoModels);
        }
    }
}
