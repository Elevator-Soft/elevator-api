using System;

namespace Elevator.Api.Dto
{
    public class CreateProjectRequestDto
    {
        public string Name { get; set; }
        public Uri ProjectUri { get; set; }
        public string GitToken { get; set; }
    }
}
