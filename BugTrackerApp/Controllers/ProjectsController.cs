using BugTrackerApp.Data;
using BugTrackerApp.Data.Services;
using BugTrackerApp.Models;
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
        private readonly IProjectsService _service;
        public ProjectsController(IProjectsService service)
        {
            _service = service;
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
    }
}

