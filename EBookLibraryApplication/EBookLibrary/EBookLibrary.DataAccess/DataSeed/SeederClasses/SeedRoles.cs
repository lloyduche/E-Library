using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.DataSeed.SeederClasses
{
    public static class SeedRoles
    {

        public async static Task AddRoles(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            if (!roleManager.Roles.Any())
            {
                var listOfRoles = new List<IdentityRole>
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("Regular")
                };

                foreach (var role in listOfRoles)
                {
                    await roleManager.CreateAsync(role);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
