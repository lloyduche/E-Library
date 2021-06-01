using EBookLibrary.DataAccess.EntityConfigurations;
using EBookLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EBookLibrary.DataAccess
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books {get;set;}

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EntityConfigurationForUser());
            builder.ApplyConfiguration(new EntityConfiguartionForCategory());
            builder.ApplyConfiguration(new EntityConfigurationForBooks());
            builder.ApplyConfiguration(new EntityConfigurationForRatings());
            builder.ApplyConfiguration(new EntityConfigurationForReviews());
        }
    }
}
