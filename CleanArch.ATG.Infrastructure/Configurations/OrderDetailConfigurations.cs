using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArch.ATG.Infrastructure.Configurations
{
    internal class OrderDetailConfigurations : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure( EntityTypeBuilder<OrderDetail> builder )
        {
            builder.HasKey(od => new { od.OrderId , od.ProductId });

            //builder.HasOne(od=>od.Product)
            //    .WithMany(Product => Product.OrderDetails)
            //    .HasForeignKey(od => od.ProductId);

            //builder.HasOne(od => od.Order)
            //   .WithMany(order => order.OrderDetails)
            //   .HasForeignKey(od => od.OrderId);
        }
    }
}
