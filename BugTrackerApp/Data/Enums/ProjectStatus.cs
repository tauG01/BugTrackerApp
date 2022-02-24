using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.Enums
{
    public enum ProjectStatus
    {
        New,
        Active,
        [Display(Name = "In Progress")]
        InProgress,
        Completed
    }
}
