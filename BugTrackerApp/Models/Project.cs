using BugTrackerApp.Data.Base;
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

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }
        
        //relationships
        public List<Ticket> Tickets { get; set; }
    }
}




