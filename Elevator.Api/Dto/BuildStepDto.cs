using System;
using Models;

namespace Elevator.Api.Dto
{
    public class BuildStepDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BuildStepScript BuildStepScript { get; set; }
    }
}
