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
    public static class SeedReview
    {
        public async static Task AddReview(AppDbContext context)
        {
            if (!context.Reviews.Any())
            {

                //read rating json file
                using StreamReader reviewToRead = new StreamReader("../EBookLibrary.DataAccess/JsonFiles/Review.json");
                var reviewData = await reviewToRead.ReadToEndAsync();

                // deserilization of Json object
                var reviewInfo = JsonConvert.DeserializeObject<IEnumerable<Review>>(reviewData);

                var users = context.Users.ToList();

                var books = context.Books.ToList();

                var bookCount = 0;
                var bookRateCount = 0;
                var userCount = 0;
                foreach (var review in reviewInfo)
                {
                    review.BookId = books[bookCount].Id;
                    review.UserId = users[userCount].Id;

                    bookRateCount += 1;
                    userCount += 1;

                    context.Reviews.Add(review);

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
