using BugTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.ViewModels
{
    public class NewTicketDropdownsVM
    {
        public NewTicketDropdownsVM()
        {
            Projects = new List<Project>();
        }
        public List<Project> Projects { get; set; }
    }
}
