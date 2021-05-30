using System;
using System.Collections.Generic;
using Models;

namespace Elevator.Api.Dto
{
    public class BuildDto
    {
        public Guid Id { get; set; }
        public Guid BuildConfigId { get; set; }
        public List<string> Logs { get; set; }
        public DateTime? FinishTime { get; set; }
        public BuildStatus BuildStatus { get; set; }
        public string StartedByUserId { get; set; }
    }
}
