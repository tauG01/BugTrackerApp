using BugTrackerApp.Data.Base;
using BugTrackerApp.Data.Enums;
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

        //update project status with values from Project index view scripts
        Task UpdateAsync(int projectId, int projectStatus);
    }
}
