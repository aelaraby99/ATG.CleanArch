using CleanArch.ATG.Domain.Entities;

namespace CleanArch.ATG.Infrastructure.Data
{
    public interface IFakeDataRepo
    {
        Task<Product> AddProduct( Product product );
        Task<Product> GetProductByIdAsync( int id );
        Task<IEnumerable<Product>> GetProductsAsync();
        Task DeleteProductById( int Id );
        Task UpdateProduct( Product product );
        Task EventOccured( Product product , string evt );
    }
}