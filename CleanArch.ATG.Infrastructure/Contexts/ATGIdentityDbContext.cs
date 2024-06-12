using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CleanArch.ATG.Domain.Entities.Identity;

namespace CleanArch.ATG.Infrastructure.Contexts
{
    public class ATGIdentityDbContext : IdentityDbContext<UserApplication,AppRole,Guid>
    {
        public ATGIdentityDbContext( DbContextOptions<ATGIdentityDbContext> options ) : base(options)
        {

        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating(builder);
            //builder.Entity<UserApplication>()
            //    .Property(u => u.LockoutEnabled)
            //    .HasColumnType("NUMBER(1)")
            //    .HasConversion<int>();
        }
    }
}
