using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.ATG.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository( ATGDbContext dbContext ) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Set<Product>().ToListAsync();
        }
    }
}
