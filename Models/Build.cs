using System;
using System.Collections.Generic;

namespace Models
{
    public class Build
    {
        public Guid Id { get; set; }
        public Guid BuildConfigId { get; set; }
        public List<string> Logs { get; set; }
        public DateTime? FinishTime { get; set; }
        public BuildStatus BuildStatus { get; set; }
        public string StartedByUserId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
