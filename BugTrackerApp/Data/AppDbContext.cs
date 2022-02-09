using BugTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.ProjectId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
