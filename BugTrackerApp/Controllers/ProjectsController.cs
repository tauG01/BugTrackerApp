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
using static BugTrackerApp.Helper;

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

        [NoDirectAccess] //Preventing direct access from URL, Logic implemented in Helper class
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
               // return View(project);
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", project) });
            }
            else
            {
                await _service.AddAsync(project);
                //notification
                TempData["success"] = "Project created successfully";
            }
            // return RedirectToAction(nameof(Index));
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", await _service.GetAllAsync(n => n.Tickets)) });

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
                //return View(project);
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", project) });
            }

            await _service.UpdateAsync(id, project);
            //notification
            TempData["success"] = "Project updated successfully";
            // return RedirectToAction(nameof(Index));
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", await _service.GetAllAsync(n => n.Tickets)) });
        }

        //Get: Pojects/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var projectDetails = await _service.GetByIdAsync(id);
            if (projectDetails == null) return View("NotFound");
            return View(projectDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var projectDetails = await _service.GetByIdAsync(id);
            if (projectDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            //notification
            TempData["success"] = "Project deleted successfully";
            //return RedirectToAction(nameof(Index));
            return Json(new { html = Helper.RenderRazorViewToString(this, "Index", await _service.GetAllAsync(n => n.Tickets)) });
        }

        //update project status with values from Project index view scripts
        public async Task<IActionResult> UpdateStatus(int ProjectId, int ProjectStatus)
        {
            await _service.UpdateAsync(ProjectId, ProjectStatus);
            //notification
            TempData["success"] = "Project status updated successfully";
            return RedirectToAction(nameof(Index));
        }

        //Add project users

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

                if (await _userManager.IsInRoleAsync(user, project.Name))
                {
                    projectUsersVM.Selected = true;
                }
                else
                {
                    projectUsersVM.Selected = false;
                }
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
                ViewBag.ErrorMessage = $"Project with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await _userManager.FindByIdAsync(model[i].UserId);

                    IdentityResult result;
                    if (model[i].Selected && !await _userManager.IsInRoleAsync(user, project.Name))
                    {
                       // result = await _userManager.AddToRoleAsync(user, project.Name);
                        result = await _userManager.AddToRoleAsync(user, project.Name);
                    }
                    else if (!model[i].Selected && (await _userManager.IsInRoleAsync(user, project.Name)))
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, project.Name);
                    }
                    else
                    {
                        continue;
                    }

                    if (result.Succeeded)
                    {
                        if (i < (model.Count - 1))
                            continue;
                        else
                            return RedirectToAction("Details", new { Id = id });
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}


