using CleanArch.ATG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Infrastructure.Contexts
{
    public class ATGDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        //public DbSet<Library> Libraries { get; set; }

        public ATGDbContext( DbContextOptions<ATGDbContext> options ) : base(options)
        {
        }
        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
