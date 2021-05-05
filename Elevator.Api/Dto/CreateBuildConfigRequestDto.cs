using System;

namespace Elevator.Api.Dto
{
    public class CreateBuildConfigRequestDto
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}\n" +
                   $"{nameof(ProjectId)} : {ProjectId}";
        }
    }
}
