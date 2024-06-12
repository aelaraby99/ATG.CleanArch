
namespace CleanArch.ATG.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        IProductRepository ProductRepository { get; }
        Task CompleteAsync();
    }
}
