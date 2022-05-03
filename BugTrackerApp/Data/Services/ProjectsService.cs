using BugTrackerApp.Data.Base;
using BugTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.Services
{
    public class ProjectsService : EntityBaseRepository <Project>, IProjectsService
    {
        private readonly AppDbContext _context;
        public ProjectsService(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var projectDetails = await _context.Projects
                .Where(proj => proj.Id == id)
                .Include(proj => proj.Tickets)
                .Include(proj => proj.ApplicationUsers)
                .FirstOrDefaultAsync(n => n.Id == id);
            return projectDetails;
        }

        //update project status with values from Project index view scripts
        public async Task UpdateAsync(int projectId, int projectStatus)
        {
            var dbProject = await _context.Projects.FirstOrDefaultAsync(n => n.Id == projectId);
            if (dbProject != null)
            {
                dbProject.ProjectStatus = (Enums.ProjectStatus)projectStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
