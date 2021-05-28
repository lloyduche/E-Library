using EBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DataAccess.EntityConfigurations
{
    public class EntityConfigurationForBooks : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(user => user.Ratings)
               .WithOne(rating => rating.Book)
               .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(user => user.Reviews)
                .WithOne(review => review.Book)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
