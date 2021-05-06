using System;

namespace Models
{
    public class BuildStep : BaseModel
    {
        public Guid BuildConfigId { get; set; }
        public BuildStepScript BuildStepScript { get; set; }
    }
}
