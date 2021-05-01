using System;
using System.Collections.Generic;

namespace Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri ProjectUri { get; set; }
        public string GitToken { get; set; }
        public List<BuildConfig> BuildConfigs { get; set; }
    }
}
