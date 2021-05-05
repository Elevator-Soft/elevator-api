using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Database.Models
{
    public class BuildConfig
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
    }
}
