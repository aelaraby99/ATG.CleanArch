using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Infrastructure.Configurations
{
    public class BrandCategoryConfiguration : IEntityTypeConfiguration<BrandCategory>
    {
        public void Configure( EntityTypeBuilder<BrandCategory> builder )
        {
            builder.HasKey(bc => new { bc.BrandId , bc.CategoryId });
            builder.HasOne(bc=>bc.Category)
                .WithMany(c=>c.BrandCategories)
                .HasForeignKey(bc=>bc.CategoryId);

            builder.HasOne(bc => bc.Brand)
                .WithMany(b => b.BrandCategories)
                .HasForeignKey(bc => bc.BrandId);
        }
    }
}
