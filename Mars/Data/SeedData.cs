using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Data
{
    public static class SeedData
    {
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration config)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string name = config["Data:AdminUser:Name"];
            string email = config["Data:AdminUser:Email"];
            string password = config["Data:AdminUser:Password"];
            string role = config["Data:AdminUser:Role"];

            // check if user exists
            if (await userManager.FindByNameAsync(name) == null)
            {
                // check if the role exists
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    // add the role if missing
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                // create our user
                IdentityUser user = new IdentityUser
                {
                    UserName = name,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
