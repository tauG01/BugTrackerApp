using BugTrackerApp.Data.Base;
using BugTrackerApp.Data.ViewModels;
using BugTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.Services
{
    public class TicketsService : EntityBaseRepository<Ticket>, ITicketsService
    {
        private readonly AppDbContext _context;
        public TicketsService(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task AddNewTicketAsync(NewTicketVM data)
        {
            var newTicket = new Ticket()
            {
                //Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                Created = data.Created,
                Modified = data.Modified,
                ProjectId = data.ProjectId
            };
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            var ticketDetails = await _context.Tickets
                .Include(p => p.Project)
                .FirstOrDefaultAsync(n => n.Id == id);
            return ticketDetails;
        }

        public async Task<NewTicketDropdownsVM> GetNewTicketDropdownsValues()
        {
            var response = new NewTicketDropdownsVM()
            {
                Projects = await _context.Projects.OrderBy(n => n.Name).ToListAsync()
            };
            return response;
        }

        public async Task UpdateTicketAsync(NewTicketVM data)
        {
            var dbTicket = await _context.Tickets.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbTicket != null)
            {
                dbTicket.Title = data.Title;
                dbTicket.Description = data.Description;
                dbTicket.Modified = data.Modified;
                dbTicket.ProjectId = data.ProjectId;
                await _context.SaveChangesAsync();
            }

        }
        //update ticket status with values from Ticket index view scripts
        public async Task UpdateAsync(int ticketId, int ticketStatus)
        {
            var dbTicket = await _context.Tickets.FirstOrDefaultAsync(n => n.Id == ticketId);
            if (dbTicket != null)
            {
                dbTicket.TicketStatus = (Enums.TicketStatus)ticketStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}