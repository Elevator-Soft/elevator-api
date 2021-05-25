using System.Collections.Generic;

namespace Repositories.Database.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsRegistered { get; set; }

        public List<string> ProjectsWithAdminAccess { get; set; }

        public List<string> ProjectsWithUserAccess { get; set; }
    }
}
