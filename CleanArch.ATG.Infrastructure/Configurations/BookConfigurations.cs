using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArch.ATG.Infrastructure.Configurations
{
    internal class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure( EntityTypeBuilder<Book> builder )
        {
            builder.HasKey(b => b.BookId);
            //builder.HasOne(b => b.Library)
            //    .WithMany(l => l.Books)
            //    .HasForeignKey(Library => Library.LibraryId);
        }
    }
}
