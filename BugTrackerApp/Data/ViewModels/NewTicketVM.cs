using BugTrackerApp.Data;
using BugTrackerApp.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Models
{
    public class NewTicketVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        //public string Submitter { get; set; }
        //public string Assignee { get; set; }
        //public string Status { get; set; }
        //public string Severity { get; set; }

        //relationships
        //project
        [Display(Name = "Select a project")]
        [Required(ErrorMessage = "Ticket project is required")]
        public int ProjectId { get; set; }
    }
}