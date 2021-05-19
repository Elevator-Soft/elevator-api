using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Dto;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Elevator.Api.Controllers
{
    public class BuildController : ControllerBase
    {
        private readonly ILogger<BuildController> logger;
        private readonly IBuildService buildService;

        public BuildController(IBuildService buildService, ILogger<BuildController> logger)
        {
            this.buildService = buildService;
            this.logger = logger;
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/builds/{buildId:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<BuildDto>> GetByIdAsync([FromRoute]Guid buildConfigId, [FromRoute]Guid buildId)
        {
            logger.LogInformation($"Start execution method '{nameof(GetByIdAsync)}'");

            var build = await buildService.GetByIdAsync(buildConfigId, buildId);
            return HttpOperationResult<BuildDto>.Ok(ModelsMapper.ConvertBuildServiceModelToDto(build));
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/builds/last")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<BuildDto>> GetLastAsync([FromRoute] Guid buildConfigId)
        {
            logger.LogInformation($"Start execution method '{nameof(GetLastAsync)}'");

            var build = await buildService.GetLastBuildAsync(buildConfigId);
            return HttpOperationResult<BuildDto>.Ok(ModelsMapper.ConvertBuildServiceModelToDto(build));
        }

        [HttpGet("projects/{projectId:guid}/buildConfigs/{buildConfigId:guid}/builds")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<List<BuildDto>>> GetAfterDateTimeAsyncTask([FromRoute] Guid buildConfigId, [FromQuery] DateTime afterDateTime)
        {
            logger.LogInformation($"Start execution method '{nameof(GetAfterDateTimeAsyncTask)}'");

            var builds = await buildService.GetAllBuildsAfterDateAsync(buildConfigId, afterDateTime);
            var dtoModels = builds.Select(ModelsMapper.ConvertBuildServiceModelToDto).ToList();
            return HttpOperationResult<List<BuildDto>>.Ok(dtoModels);
        }
    }
}
