using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace CleanArch.ATG.Infrastructure.Configurations
{
    internal class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure( EntityTypeBuilder<Book> builder )
        {
            builder.HasKey(b => b.BookId); // Primary Key [Unique , Not Null]
            builder.Property(b => b.Code)
                .IsRequired(); // Not Null

            builder.HasIndex(b => new { b.Code , b.AuthorName })
                .IsUnique(); //Unique value

            //builder.Property(b => b.Price)
            //    .HasDefaultValue(6.5); // Default value
            ////Ck_Price check( Price >= 5.5)
            //builder.HasCheckConstraint("Ck_Price" , "\"Price\" >= 6.5"); // Check Constraint

            //builder.HasCheckConstraint("Ck_Account" , "AccNumber > 10");





            //builder.HasOne(b => b.Library)
            //    .WithMany(l => l.Books)
            //    .HasForeignKey(Library => Library.LibraryId);
        }
    }
}
