using System;

namespace Elevator.Api.Models
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }
        public Uri ProjectUri { get; set; }
        public string GitToken { get; set; }
    }
}
