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
        public static Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration config)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = new IdentityUser
            {
                UserName = config["Data:AdminUser:Name"],
                Email = config["Data:AdminUser:Email"]
            };
        }
    }
}
