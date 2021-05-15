using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Repositories.Database.Models
{
    public class Build
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid BuildConfigId { get; set; }
        public List<string> Logs { get; set; }
        public DateTime FinishTime { get; set; }
        public BuildStatus BuildStatus { get; set; }
    }
}
