using EBookLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.DataSeed.SeederClasses
{
    public static class SeedUser
    {
        public async static Task AddUser(AppDbContext context,UserManager<User> userManager)
        {
            //add user to a role
            if (!userManager.Users.Any())
            {

                //read json file
                using StreamReader userToRead = new StreamReader("../EBookLibrary.DataAccess/JsonFiles/User.json");
                var userData = await userToRead.ReadToEndAsync();

                // deserilization of Json object
                var usersInfo = JsonConvert.DeserializeObject<IEnumerable<User>>(userData);



                int counter = 1;
                string userType;
                string msg = string.Empty;

                foreach (var user in usersInfo)
                {
                    if (counter < 2)
                    {
                        userType = "Admin";

                        var result = await userManager.CreateAsync(user, "P@ssword12");

                        var userRole = await userManager.AddToRoleAsync(user, userType);
                    }

                    else
                    {
                        userType = "Regular";
                        var regularUser = await userManager.CreateAsync(user, "P@assword123");
                        var regularUserRole = await userManager.AddToRoleAsync(user, userType);
                    }

                    counter += 1;
                }

                await context.SaveChangesAsync();
            }

        }
    }
}
