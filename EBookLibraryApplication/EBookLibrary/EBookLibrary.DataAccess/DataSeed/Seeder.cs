using EBookLibrary.DataAccess.DataSeed.SeederClasses;
using EBookLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.DataSeed
{
    public static class Seeder
    {
        public async static Task Seed(AppDbContext context,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            
            context.Database.EnsureCreated();

            //create roles

            await SeedRoles.AddRoles(roleManager, context);



            //Add User

            await SeedUser.AddUser(context,userManager);

            // Add User

            await SeedCategories.AddCategories(context);
            //Add Ratings

            await SeedRatings.AddRatings(context);


            //Add Review
            await SeedReview.AddReview(context);


        }
    }
}
