using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Hospital.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while migrating database: {ex.Message}");
            }

            try
            {
                if (!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Roles.Patient)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Roles.Doctor)).GetAwaiter().GetResult();

                    var result = _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "Sarfaraz",
                        Email = "sarfaraz@abc.com"
                    }, "Sarfaraz@123").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        var appUser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "sarfaraz@abc.com");
                        if (appUser != null)
                        {
                            _userManager.AddToRoleAsync(appUser, Roles.Admin).GetAwaiter().GetResult();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to create user. Errors:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }
    }
}
