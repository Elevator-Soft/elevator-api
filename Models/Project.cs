using System;

namespace Models
{
    public class Project : BaseModel
    {
        public Uri ProjectUri { get; set; }
        public string GitToken { get; set; }
    }
}
