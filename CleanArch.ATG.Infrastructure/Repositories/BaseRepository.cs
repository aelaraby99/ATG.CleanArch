using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.ATG.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ATGDbContext _dbContext;
        internal DbSet<TEntity> _dbSet;
        public BaseRepository( ATGDbContext dbContext )
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync( TEntity entity )
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async Task DeleteById( int Id )
        {
            var entityToDelete = await GetByIdAsync(Id);
            if (entityToDelete != null)
                await Delete(entityToDelete);
        }

        public Task Delete( TEntity entity )
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Attach(entity);
            }
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync( int Id )
        {
            return await _dbSet.FindAsync(Id);
        }

        public Task Update( TEntity entity )
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public List<BookByAuthor> GetBooksByAuthor( string authorName )
        {
            return _dbContext.Set<BookByAuthor>().FromSqlRaw("EXEC GetBooks @P_AuthorName" , authorName).ToList();
        }
    }
}
