﻿using Middleware.Models;

namespace References.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}