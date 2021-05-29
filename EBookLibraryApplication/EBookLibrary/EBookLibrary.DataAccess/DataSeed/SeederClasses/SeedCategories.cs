using EBookLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookLibrary.DataAccess.DataSeed.SeederClasses
{
    public static class SeedCategories
    {

        public async static Task AddCategories(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                //read json file
                using StreamReader categoryToRead = new StreamReader("../EBookLibrary.DataAccess/JsonFiles/Category.json");
                var categoryData = await categoryToRead.ReadToEndAsync();


                // deserilization of Json object
                var categoryInfo = JsonConvert.DeserializeObject<List<Category>>(categoryData);

                context.Categories.AddRange(categoryInfo);
                await context.SaveChangesAsync();
            }
        }
    }
}
