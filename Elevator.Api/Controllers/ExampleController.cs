using System;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Api.Context;
using Elevator.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("Example")]
    public class ExampleController : ControllerBase
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
            await dbContext.example.AddAsync(new Example {Id = id});
            await dbContext.SaveChangesAsync();
            return id;
        }
    }
}
