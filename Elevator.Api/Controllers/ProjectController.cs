using System;
using System.Threading.Tasks;
using Elevator.Api.Models;
using Elevator.Api.Services;
using Elevator.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("Project")]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<OperationResult<Project>> Create([FromBody] CreateProjectRequest createProjectRequest)
        {
            var project = await projectService.CreateAsync(createProjectRequest);
            return OperationResult<Project>.Created(project);
        }
    }
}
