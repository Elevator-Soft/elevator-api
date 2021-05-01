using System;
using System.Collections.Generic;

namespace Models
{
    public class BuildConfig
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<BuildStep> BuildSteps { get; set; }
    }
}
