using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Dto;
using Elevator.Api.Exceptions;
using Elevator.Api.Extensions.Dto;
using Elevator.Api.Services;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Elevator.Api.Controllers
{
    [ApiController]
    public class BuildConfigController : ControllerBase
    {
        private readonly ILogger<BuildConfigController> logger;
        private readonly IBuildConfigService buildConfigService;

        public BuildConfigController(ILogger<BuildConfigController> logger, IBuildConfigService buildConfigService)
        {
            this.logger = logger;
            this.buildConfigService = buildConfigService;
        }

        [HttpPost("projects/{projectId:guid}/buildConfigs")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<BuildConfigDto>> CreateAsync([FromBody] CreateBuildConfigRequestDto createBuildConfigRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(CreateAsync)}'");
            logger.LogInformation($"CreateProjectRequestDto: '{createBuildConfigRequestDto}'");

            var buildConfig =
                await buildConfigService.CreateAsync(createBuildConfigRequestDto.ToServiceModel());

            var dtoModel = ModelsMapper.ConvertServiceBuildConfigModelToDto(buildConfig);
            return HttpOperationResult<BuildConfigDto>.Created(dtoModel);
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<List<BuildConfigDto>>> GetAllAsync([FromRoute]Guid projectId)
        {
            logger.LogInformation($"Start execution method '{nameof(GetAllAsync)}'");

            var buildConfigs = await buildConfigService.GetAllFromProjectAsync(projectId);
            var dtoModels = buildConfigs.Select(ModelsMapper.ConvertServiceBuildConfigModelToDto).ToList();

            return HttpOperationResult<List<BuildConfigDto>>.Ok(dtoModels);
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs/{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<BuildConfigDto>> GetAsync([FromRoute] Guid projectId, [FromRoute] Guid id)
        {
            //todo(likvidator): это костыль, да (:

            logger.LogInformation($"Start execution method '{nameof(GetAsync)}'. Id={id}");
            var buildConfigs = (await buildConfigService.GetAllFromProjectAsync(projectId))
                .Select(ModelsMapper.ConvertServiceBuildConfigModelToDto).ToList();
            var result = buildConfigs.FirstOrDefault(bc => bc.Id == id);

            if (result == null)
                throw new EntityNotFoundException(nameof(BuildConfig), id.ToString());

            return HttpOperationResult<BuildConfigDto>.Ok(result);
        }
    }
}
