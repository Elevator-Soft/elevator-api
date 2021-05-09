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
        public async Task<OperationResult<CreateProjectResultDto>> CreateAsync([FromBody] CreateProjectRequestDto createProjectRequestDto)
        {
            logger.LogInformation($"Start execution method '{nameof(CreateAsync)}'");
            logger.LogInformation($"CreateProjectRequestDto: '{createProjectRequestDto}'");

            if (!createProjectRequestDto.GitUrl.ToString().StartsWith("https://"))
                return OperationResult<CreateProjectResultDto>.BadRequest("Elevator only support git url witch starts with 'https://'");

            var project = await projectService.CreateAsync(createProjectRequestDto.ToServiceProject());
            var resultDto = new CreateProjectResultDto
            {
                Id = project.Id
            };

            return OperationResult<CreateProjectResultDto>.Created(resultDto);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<OperationResult<List<ProjectDto>>> GetAllAsync()
        {
            logger.LogInformation($"Start execution method '{nameof(GetAllAsync)}'");
            var projects = await projectService.GetAllAsync();
            var dtoModels = projects.Select(ModelsMapper.ConvertProjectServiceModelToDtoModel).ToList();
            return OperationResult<List<ProjectDto>>.Ok(dtoModels);
        }
    }
}
