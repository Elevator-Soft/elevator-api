using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Database.Models
{
    public class BuildStep
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BuildConfigId { get; set; }
        public Guid BuildStepScriptId { get; set; }
        public BuildStepScript BuildStepScript { get; set; }
    }
}
