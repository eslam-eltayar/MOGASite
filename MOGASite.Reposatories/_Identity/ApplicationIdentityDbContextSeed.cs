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
        public static async Task SeedUserAsync(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new IdentityUser()
                {
                    Email = "adminmagdelislam@admin.com",
                    UserName = "magdelislam",

                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
