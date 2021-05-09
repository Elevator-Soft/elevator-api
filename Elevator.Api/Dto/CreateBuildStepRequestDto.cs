using System;
using Models;

namespace Elevator.Api.Dto
{
    public class CreateBuildStepRequestDto
    {
        public string Name { get; set; }
        public Guid BuildConfigId { get; set; }
        public BuildStepScript BuildStepScript { get; set; }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}\n" +
                   $"{nameof(BuildStepScript)} : {BuildStepScript}";
        }
    }
}
