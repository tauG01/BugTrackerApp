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
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        //public string Submitter { get; set; }
        //public string Assignee { get; set; }
        //public string Status { get; set; }
        //public string Severity { get; set; }

        //relationships
        //project
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}