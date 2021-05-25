using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elevator.Api.Dto
{
    [JsonObject]
    public class UserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsRegistered { get; set; }

        public ProjectAccesses ProjectAccesses { get; set; }
    }

    [JsonObject]
    public class ProjectAccesses
    {
        public IReadOnlyList<string> Admin { get; set; }

        public IReadOnlyList<string> User { get; set; }
    }
}
