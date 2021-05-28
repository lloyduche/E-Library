using EBookLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EBookLibrary.DataAccess
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Book> Books {get;set;}

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Review> Reviews { get; set; }

             
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
