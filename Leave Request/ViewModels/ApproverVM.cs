﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_Request.ViewModels
{
    public class ApproverVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}