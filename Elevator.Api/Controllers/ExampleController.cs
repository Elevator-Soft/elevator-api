using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories.Database.Models;
using Repositories.Repositories;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("Example")]
    public class ExampleController : Controller
    {
        private readonly ExampleRepository repository;

        public ExampleController(ExampleRepository repository)
        {
            this.repository = repository;
        }

        [Route("example")]
        public async Task<int> Index()
        {
            var id = new Random().Next();
            await repository.AddAsync(new Example());
            return id;
        }
    }
}
