using System;
using System.Linq;
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
        public Guid Index()
        {
            var id = Guid.Parse("0daa5ab6-cdc7-458c-9413-07579441190c");
            dbContext.example.Add(new ExampleDB {Id = id});
            return dbContext.example.Find(id).Id;
        }
    }
}
