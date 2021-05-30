using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Dto;
using Elevator.Api.Extensions.Dto;
using Elevator.Api.Services;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<ProjectDto>> CreateAsync([FromBody] CreateProjectRequestDto createProjectRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(CreateAsync)}'");
            logger.LogInformation($"CreateProjectRequestDto: '{createProjectRequestDto}'");

            if (!createProjectRequestDto.GitUrl.ToString().StartsWith("https://"))
                return HttpOperationResult<ProjectDto>.BadRequest("Elevator only support git url witch starts with 'https://'");

            var project = await projectService.CreateAsync(createProjectRequestDto.ToServiceProject(), CurrentUser);
            var resultDto = ModelsMapper.ConvertProjectServiceModelToDtoModel(project);

            return HttpOperationResult<ProjectDto>.Created(resultDto);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<ProjectDto>> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateProjectRequestDto updateProjectRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(UpdateAsync)}'");
            logger.LogInformation($"UpdateProjectRequestDto: '{updateProjectRequestDto}'");

            if (!updateProjectRequestDto.GitUrl.ToString().StartsWith("https://"))
                return HttpOperationResult<ProjectDto>.BadRequest("Elevator only support git url witch starts with 'https://'");

            var project = updateProjectRequestDto.ToServiceProject();
            project.Id = id;
            var updated = await projectService.UpdateAsync(project, CurrentUser);
            var resultDto = ModelsMapper.ConvertProjectServiceModelToDtoModel(updated);

            return HttpOperationResult<ProjectDto>.Ok(resultDto);
        }

        [HttpPost("{id:guid}/grantAccess")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpVoidOperationResult> GrantAccessAsync([FromBody] GrantAccessRequestDto grantAccessRequest)
        {
            logger.LogInformation($"Start execution method '{nameof(GrantAccessAsync)}'");
            logger.LogInformation($"GrantAccessRequestDto: '{grantAccessRequest}'");

            await projectService.GrantAccess(grantAccessRequest.ProjectId, CurrentUser, grantAccessRequest.UserId,
                grantAccessRequest.AccessType);

            return HttpVoidOperationResult.Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<List<ProjectDto>>> GetAllAsync()
        {
            logger.LogInformation($"Start execution method '{nameof(GetAllAsync)}'");
            var projects = await projectService.GetAllAsync(CurrentUser);
            var dtoModels = projects.Select(ModelsMapper.ConvertProjectServiceModelToDtoModel).ToList();
            return HttpOperationResult<List<ProjectDto>>.Ok(dtoModels);
        }
        
        [HttpGet("{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<ProjectDto>> GetAsync([FromRoute]Guid id)
        {
            logger.LogInformation($"Start execution method '{nameof(GetAsync)}'. Id = '{id}'");
            var project = await projectService.GetByIdAsync(id, CurrentUser);
            var dtoModel = ModelsMapper.ConvertProjectServiceModelToDtoModel(project);
            return HttpOperationResult<ProjectDto>.Ok(dtoModel);
        }
    }
}
