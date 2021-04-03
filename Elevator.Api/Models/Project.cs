﻿using System;

namespace Elevator.Api.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri ProjectUri { get; set; }
        public string GitToken { get; set; }
    }
}
