using System;

namespace Elevator.Api.Dto
{
    public class CreateBuildConfigRequestDto
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
    }
}
