﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name ="Last name")]
        public string LastName { get; set; }
    }
}
