using BugTrackerApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerApp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Projects
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(new List<Project>()
                    {
                        new Project()
                        {
                            Name = "Project 1",
                            Description = "This is the description of the first Project",
                            Owner = "Owner 1"
                        },
                        new Project()
                        {
                            Name = "Project 2",
                            Description = "This is the description of the second Project",
                            Owner = "Owner 2"
                        },
                        new Project()
                        {
                            Name = "Project 3",
                            Description = "This is the description of the third Project",
                            Owner = "Owner 3"
                        },
                        new Project()
                        {
                            Name = "Project 4",
                            Description = "This is the description of the fourth Project",
                            Owner = "Owner 4"
                        },
                        new Project()
                        {
                            Name = "Project 6",
                            Description = "This is the description of the fifth Project",
                            Owner = "Owner 5"
                        },
                    });
                    context.SaveChanges();
                }

                //Cinema
                if (!context.Tickets.Any())
                {
                    context.Tickets.AddRange(new List<Ticket>()
                    {
                        new Ticket()
                        {
                            Title = "Ticket 1",
                            Description = "This is the description of the first Ticket",
                            Created = DateTime.Now,
                            ProjectId = 1
                        },
                        new Ticket()
                        {
                            Title = "Ticket 2",
                            Description = "This is the description of the second Ticket",
                            Created = DateTime.Now,
                            ProjectId = 2
                        },
                        new Ticket()
                        {
                            Title = "Ticket 3",
                            Description = "This is the description of the third Ticket",
                            Created = DateTime.Now,
                            ProjectId = 3
                        },
                        new Ticket()
                        {
                            Title = "Ticket 4",
                            Description = "This is the description of the fourth Ticket",
                            Created = DateTime.Now,
                            ProjectId = 4
                        },
                        new Ticket()
                        {
                            Title = "Ticket 6",
                            Description = "This is the description of the fifth Ticket",
                            Created = DateTime.Now,
                            ProjectId = 5
                        },
                    });
                    context.SaveChanges();
                }
            }

        }
    }
}
