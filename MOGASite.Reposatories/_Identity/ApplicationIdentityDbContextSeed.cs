using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Reposatories._Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            string[] roleNames = { "Admin", "Marketer" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            // Check for admin user
            var adminUser = await userManager.FindByEmailAsync("adminmagdelislam@admin.com");

            if (adminUser == null)
            {
                var user = new IdentityUser()
                {
                    Email = "adminmagdelislam@admin.com",
                    UserName = "magdelislam",
                    
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

            }
            else if (!(await userManager.IsInRoleAsync(adminUser, "Admin")))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }


            // Check for marketer user

            var marketerUser = await userManager.FindByEmailAsync("marketer@admin.com");

            if (marketerUser == null)
            {
                var user = new IdentityUser()
                {
                    Email = "marketer@admin.com",
                    UserName = "marketer",
      
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Marketer");
                }

            }
            else if (!(await userManager.IsInRoleAsync(marketerUser, "Marketer")))
            {
                await userManager.AddToRoleAsync(marketerUser, "Marketer");
            }
        }
    }
}
