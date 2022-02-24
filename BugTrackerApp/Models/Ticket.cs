using BugTrackerApp.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Models
{
    public class Ticket: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Bug Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        //public DateTime Created { get { return DateTime.Now; } }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        //[Display(Name = "Due Date")]
        //public DateTime DueDate { get; set; }
        //public string Reporter { get; set; }
        //public string Assignee { get; set; }
        //public string Status { get; set; } //Open | Closed
        //public string Severity { get; set; } //High | Medium | Low

        //relationships
        //project
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}