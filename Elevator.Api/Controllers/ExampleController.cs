using System;
using System.Threading.Tasks;
using Elevator.Api.Database;
using Elevator.Api.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("Example")]
    public class ExampleController : Controller
    {
        private readonly DatabaseContext dbContext;

        public ExampleController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("example")]
        public async Task<Guid> Index()
        {
            var id = Guid.NewGuid();
            await dbContext.Example.AddAsync(new Example {Id = id});
            await dbContext.SaveChangesAsync();
            return id;
        }
    }
}
