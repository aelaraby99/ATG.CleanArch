using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArch.ATG.Infrastructure.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure( EntityTypeBuilder<Product> builder )
        {
            builder.HasKey(p => p.Id);

            //builder.HasOne(p => p.Category)
            //    .WithMany(c => c.Products)
            //    .HasForeignKey(p => p.CategoryId);

            //builder.HasOne(p => p.Brand)
            //    .WithMany(b => b.Products)
            //    .HasForeignKey(p => p.BrandId);
        }
    }
}
