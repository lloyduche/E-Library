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
    public static class SeedRatings
    {

        public async static Task AddRatings(AppDbContext context)
        {
            if (!context.Ratings.Any())
            {

                //read rating json file
                using StreamReader ratingsToRead = new StreamReader("../EBookLibrary.DataAccess/JsonFiles/Ratings.json");
                var ratingsData = await ratingsToRead.ReadToEndAsync();

                // deserilization of Json object
                var ratingsInfo = JsonConvert.DeserializeObject<IEnumerable<Rating>>(ratingsData);

                var users = context.Users.ToList();

                var books = context.Books.ToList();

                var bookCount = 0;
                var bookRateCount = 0;
                var userCount = 0;
                foreach (var rating in ratingsInfo)
                {
                    rating.BookId = books[bookCount].Id;
                    rating.UserId = users[userCount].Id;

                    bookRateCount += 1;
                    userCount += 1;

                    context.Ratings.Add(rating);

                    if (userCount == users.Count - 1)
                    {
                        userCount = 0;
                    }

                    if (bookRateCount == 2)
                    {
                        bookCount += 1;
                        bookRateCount = 0;
                    }

                    if (bookCount == books.Count - 1)
                    {
                        bookCount = 0;
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
