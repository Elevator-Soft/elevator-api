using System;
using System.Threading.Tasks;
using Elevator.Api.Services;
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

        [Route("create")]
        public async Task<Guid> Create()
        {
            var result = await projectService.CreateAsync();
            return result.Id;
        }
    }
}
