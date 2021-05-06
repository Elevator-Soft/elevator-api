using Repositories.Database.Models;

namespace Elevator.Api.Dto
{
    public class CreateBuildStepRequestDto
    {
        public string Name { get; set; }
        public BuildStepScript BuildStepScript { get; set; }
    }
}
