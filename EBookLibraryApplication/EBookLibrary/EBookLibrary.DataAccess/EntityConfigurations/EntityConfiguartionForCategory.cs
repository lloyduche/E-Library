using EBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DataAccess.EntityConfigurations
{
    class EntityConfiguartionForCategory : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasMany(category => category.Books)
              .WithOne(book => book.Category )
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
