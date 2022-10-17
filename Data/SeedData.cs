using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using on_e_commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace on_e_commerce.Data
{
    public static class SeedData
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context=new dbEticaretContext(serviceProvider.GetRequiredService<DbContextOptions<dbEticaretContext>>()))
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

                await roleManager.CreateAsync(new AppRole { Name = "User" });
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
                await roleManager.CreateAsync(new AppRole { Name = "Manager" });
                await roleManager.CreateAsync(new AppRole { Name = "Editor" });

                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                var admin = new AppUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", EmailConfirmed = true };
                var user = new AppUser { UserName = "user@gmail.com", Email = "user@gmail.com", EmailConfirmed = true };
                var manager = new AppUser { UserName = "manager@gmail.com", Email = "manager@gmail.com", EmailConfirmed = true };
                var editor = new AppUser { UserName = "editor@gmail.com", Email = "editor@gmail.com", EmailConfirmed = true };

                await userManager.CreateAsync(admin, "admin1234");
                await userManager.CreateAsync(user, "user1234");
                await userManager.CreateAsync(manager, "manager1234");
                await userManager.CreateAsync(editor, "editor1234");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(user, "User");
                await userManager.AddToRoleAsync(manager, "Manager");
                await userManager.AddToRoleAsync(editor, "Editor");

            }

        }
    }
}
