using EBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DataAccess.EntityConfigurations
{
    public class EntityConfigurationForReviews : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(review => review.Book)
               .WithMany(review => review.Reviews)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(review => review.User)
              .WithMany(review => review.Reviews)
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
