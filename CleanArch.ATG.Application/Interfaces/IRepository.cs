
using CleanArch.ATG.Domain.Entities;

namespace CleanArch.ATG.Application.Interfaces
{
    public interface IRepository<TEntity> 
    {
        Task<TEntity> AddAsync( TEntity entity );
        Task<TEntity?> GetByIdAsync( int Id );
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task Update( TEntity entity );
        Task DeleteById( int Id );
        Task Delete( TEntity entity );
    }
}
