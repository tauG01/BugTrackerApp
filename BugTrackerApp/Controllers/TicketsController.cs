using BugTrackerApp.Data;
using BugTrackerApp.Data.Services;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsService _service;
        public TicketsController(ITicketsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var AllTickets = await _service.GetAllAsync(n => n.Project);
            return View(AllTickets);
        }

        //GET Tickets/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var ticketDetail = await _service.GetTicketByIdAsync(id);
            return View(ticketDetail);
        }

        //Get: Ticket/Create
        public async Task<IActionResult> Create()
        {
            var ticketDropDownsData = await _service.GetNewTicketDropdownsValues();
            ViewBag.Projects = new SelectList(ticketDropDownsData.Projects, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewTicketVM Ticket)
        {
            if (!ModelState.IsValid)
            {
                var ticketDropDownsData = await _service.GetNewTicketDropdownsValues();
                ViewBag.Projects = new SelectList(ticketDropDownsData.Projects, "Id", "Name");
                return View(Ticket);
            }
            Ticket.Created = DateTime.Now;
            Ticket.Modified = DateTime.Now;
            await _service.AddNewTicketAsync(Ticket);
            return RedirectToAction(nameof(Index));
        }

        //Get: Events/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var ticketDetails = await _service.GetTicketByIdAsync(id);
            if (ticketDetails == null) return View("NotFound");
            var response = new NewTicketVM()
            {
                Id = ticketDetails.Id,
                Title = ticketDetails.Title,
                Description = ticketDetails.Description,
                //Created = ticketDetails.Created,
                ProjectId = ticketDetails.ProjectId
            };

            var ticketDropDownsData = await _service.GetNewTicketDropdownsValues();

            ViewBag.Projects = new SelectList(ticketDropDownsData.Projects, "Id", "Name");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewTicketVM Ticket)
        {
            if (id != Ticket.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var ticketDropDownsData = await _service.GetNewTicketDropdownsValues();

                ViewBag.Projects = new SelectList(ticketDropDownsData.Projects, "Id", "Name");
                return View(Ticket);
            }
            Ticket.Modified = DateTime.Now;
            await _service.UpdateTicketAsync(Ticket);
            return RedirectToAction(nameof(Index));
        }

        //Get: Tickets/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ticketDetails = await _service.GetByIdAsync(id);
            if (ticketDetails == null) return View("NotFound");
            return View(ticketDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var ticketDetails = await _service.GetByIdAsync(id);
            if (ticketDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
