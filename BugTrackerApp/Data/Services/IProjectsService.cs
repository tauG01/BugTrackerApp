using BugTrackerApp.Data.Base;
using BugTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.Services
{
    public interface IProjectsService:IEntityBaseRepository<Project>
    {
        Task<Project> GetProjectByIdAsync(int id);
    }
}
