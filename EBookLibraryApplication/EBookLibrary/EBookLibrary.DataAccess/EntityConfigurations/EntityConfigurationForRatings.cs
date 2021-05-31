using EBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EBookLibrary.DataAccess.EntityConfigurations
{
    public class EntityConfigurationForRatings : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(rating => rating.Book)
               .WithMany(rating => rating.Ratings)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rating => rating.User)
              .WithMany(rating => rating.Ratings)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
