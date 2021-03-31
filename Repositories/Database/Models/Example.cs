using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Database.Models
{
    public class Example
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
