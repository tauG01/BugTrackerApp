using BugTrackerApp.Data.Base;
using BugTrackerApp.Data.ViewModels;
using BugTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data.Services
{
    public interface ITicketsService : IEntityBaseRepository<Ticket>
    {
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<NewTicketDropdownsVM> GetNewTicketDropdownsValues();
        Task AddNewTicketAsync(NewTicketVM data);
        Task UpdateTicketAsync(NewTicketVM data);
    }
}
