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
        private readonly IProjectsService projectsService;

        public ProjectController(ProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [Route("create")]
        public async Task<Guid> Create()
        {
            var result = await projectsService.CreateAsync();
            return result.Id;
        }
    }
}
