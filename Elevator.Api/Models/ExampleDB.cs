using System;
using System.ComponentModel.DataAnnotations;

namespace Elevator.Api.Models
{
    public class ExampleDB
    {
        [Key]
        public Guid Id { get; set; }
    }
}
