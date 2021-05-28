using EBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DataAccess.EntityConfigurations
{
    public class EntityConfigurationForUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(user => user.Ratings)
                .WithOne(rating => rating.User)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(user => user.Reviews)
                .WithOne(review => review.User)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
