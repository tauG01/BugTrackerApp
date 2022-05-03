using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.ViewModels
{
    public class ProjectUsersVM
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Role { get; set; }
        public bool Selected { get; set; }
    }
}
