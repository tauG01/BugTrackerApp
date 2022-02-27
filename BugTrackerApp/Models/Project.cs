using BugTrackerApp.Data.Base;
using BugTrackerApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Models
{
    public class Project:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }

        [Display(Name = "Assign Status")]
        public ProjectStatus  ProjectStatus { get; set; }
        //[Display(Name = "Start Date")]
        // public string StartDate { get; set; }
        //[Display(Name = "End Date")]
        // public string EndDate { get; set; }

        //relationships
        public List<Ticket> Tickets { get; set; }
    }
}




