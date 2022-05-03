using BugTrackerApp.Data;
using BugTrackerApp.Data.Services;
using BugTrackerApp.Data.ViewModels;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProjectsService _service;
        public ProjectsController(IProjectsService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Tickets);
            return View(data);
        }

        //Get: Pojects/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Description, Owner")] Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }
            else
            {
                await _service.AddAsync(project);
            }
            return RedirectToAction(nameof(Index));
        }

        //GET: Projects/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var projectDetails = await _service.GetProjectByIdAsync(id);
            if (projectDetails == null) return View("NotFound");
            return View(projectDetails);
        }

        //Get: Pojects/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var projectDetails = await _service.GetByIdAsync(id);
            if (projectDetails == null) return View("NotFound");
            return View(projectDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description, Owner")] Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            await _service.UpdateAsync(id, project);
            return RedirectToAction(nameof(Index));
        }

        //Get: Pojects/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var projectDetails = await _service.GetByIdAsync(id);
            if (projectDetails == null) return View("NotFound");
            return View(projectDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var projectDetails = await _service.GetByIdAsync(id);
            if (projectDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //update project status with values from Project index view scripts
        public async Task<IActionResult> UpdateStatus(int ProjectId, int ProjectStatus)
        {
            await _service.UpdateAsync(ProjectId, ProjectStatus);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddProjectUsers(int id)
        {
            ViewBag.projectId = id;
            var project = await _service.GetByIdAsync(id);
            if (project == null)
            {
                ViewBag.ErrorMessage = $"Project with Id = {id} cannot be found";
                return View("NotFound");
            }
            ViewBag.Name = project.Name;
            var model = new List<ProjectUsersVM>();
            foreach (var user in _userManager.Users)
            {
                var projectUsersVM = new ProjectUsersVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = await GetUserRoles(user)
                };

                //if (await _userManager.IsInRoleAsync(user, project.Name))
                //{
                //    projectUsersVM.Selected = true;
                //}
                //else
                //{
                //    projectUsersVM.Selected = false;
                //}
                model.Add(projectUsersVM);
            }
            return View(model);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectUsers(List<ProjectUsersVM> model, int id)
        {
            var project = await _service.GetByIdAsync(id);
            if (project == null)
            {
                return View("NotFound");
            }

            //var users = await _userManager.GetUsersAsync(project);
            //var result = await _userManager.RemoveFromUsersAsync(project, users);
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError("", "Cannot remove project existing users");
            //    return View(model);
            //}
            //result = await _userManager.AddToUsersAsync(project, model.Where(x => x.Selected).Select(y => y.UserName));
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError("", "Cannot add selected users to project");
            //    return View(model);
            //}
            return RedirectToAction("Index");
        }
    }
}

