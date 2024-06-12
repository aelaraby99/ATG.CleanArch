using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArch.ATG.Infrastructure.Configurations
{
    internal class BrandConfigurations : IEntityTypeConfiguration<Brand>
    {
        public void Configure( EntityTypeBuilder<Brand> builder )
        {
            builder.HasKey(b => b.Id);
        }
    }
}
