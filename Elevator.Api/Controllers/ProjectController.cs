using System.Threading.Tasks;
using Elevator.Api.Dto;
using Elevator.Api.Models;
using Elevator.Api.Services;
using Elevator.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("project")]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<OperationResult<CreateProjectResultDto>> Create([FromBody] CreateProjectRequestDto createProjectRequestDto)
        {
            var project = await projectService.CreateAsync(createProjectRequestDto.ToServiceProject());
            var resultDto = new CreateProjectResultDto
            {
                Id = project.Id
            };

            return OperationResult<CreateProjectResultDto>.Created(resultDto);
        }
    }
}
