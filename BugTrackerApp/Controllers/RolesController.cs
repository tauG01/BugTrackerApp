using BugTrackerApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
       // private readonly AppDbContext _context;
        public RolesController(RoleManager<IdentityRole> roleManager /*AppDbContext context*/)
        {
            _roleManager = roleManager;
          //  _context = context;
        }

        //List all roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        //Get: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {
            if (Name != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(Name.Trim()));
                //notification
                TempData["success"] = "Role created successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        //Get: Roles/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            var roleDetails = await _roleManager.FindByIdAsync(id);
            if (roleDetails == null) return View("NotFound");
            return View(roleDetails);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditComfirmed(IdentityRole roleName)
        {
            if (roleName != null)
            {
               await _roleManager.UpdateAsync(roleName);
                //notification
                TempData["success"] = "Role updated successfully";
            }
            
            return RedirectToAction(nameof(Index));
        }

        //Get: Pojects/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return View("NotFound");
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return View("NotFound");
          
            await _roleManager.DeleteAsync(role);
            //notification
            TempData["success"] = "Role deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

