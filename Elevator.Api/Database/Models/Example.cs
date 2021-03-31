using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elevator.Api.Database.Models
{

    [Table("example")]
    public class Example
    {
        [Key]
        public Guid Id { get; set; }
    }
}
