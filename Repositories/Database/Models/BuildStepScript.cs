using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Database.Models
{
    public class BuildStepScript
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Command { get; set; }
        public string Arguments { get; set; }
    }
}
