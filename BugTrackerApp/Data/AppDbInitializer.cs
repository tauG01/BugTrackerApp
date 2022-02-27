using BugTrackerApp.Data.Static;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
                           // Created = DateTime.Now,
                            ProjectId = 1
                        },
                        new Ticket()
                        {
                            Title = "Ticket 2",
                            Description = "This is the description of the second Ticket",
                           // Created = DateTime.Now,
                            ProjectId = 2
                        },
                        new Ticket()
                        {
                            Title = "Ticket 3",
                            Description = "This is the description of the third Ticket",
                          //  Created = DateTime.Now,
                            ProjectId = 3
                        },
                        new Ticket()
                        {
                            Title = "Ticket 4",
                            Description = "This is the description of the fourth Ticket",
                          //  Created = DateTime.Now,
                            ProjectId = 4
                        },
                        new Ticket()
                        {
                            Title = "Ticket 6",
                            Description = "This is the description of the fifth Ticket",
                          //  Created = DateTime.Now,
                            ProjectId = 5
                        },
                    });
                    context.SaveChanges();
                }
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.ProjectManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectManager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Programmer))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Programmer));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@bugtracker.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string projectManagerUserEmail = "pmanager@bugtracker.com";

                var projectManagerUser = await userManager.FindByEmailAsync(projectManagerUserEmail);
                if (projectManagerUser == null)
                {
                    var newProjectManagerUser = new ApplicationUser()
                    {
                        FirstName = "ProjectManager",
                        LastName = "User",
                        UserName = "pmanager-user",
                        Email = projectManagerUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newProjectManagerUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newProjectManagerUser, UserRoles.ProjectManager);
                }

                string programmerUserEmail = "programmer@bugtracker.com";

                var programmerUser = await userManager.FindByEmailAsync(programmerUserEmail);
                if (programmerUser == null)
                {
                    var newProgrammerUser = new ApplicationUser()
                    {
                        FirstName = "Programmer",
                        LastName = "User",
                        UserName = "programmer-user",
                        Email = programmerUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newProgrammerUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newProgrammerUser, UserRoles.Programmer);
                }

                string appUserEmail = "user@bugtracker.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FirstName = "Application",
                        LastName = "User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
