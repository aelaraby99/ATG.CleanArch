using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Infrastructure
{
    public class FakeDataRepo : IFakeDataRepo
    {
        private static List<Product> _products;
        public FakeDataRepo()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.5 },
                new Product { Id = 2, Name = "Product 2", Price = 20.5 },
                new Product { Id = 3, Name = "Product 3", Price = 30.5 },
                new Product { Id = 4, Name = "Product 4", Price = 40.5 },
                new Product { Id = 5, Name = "Product 5", Price = 50.5 },
            };
        }
        public async Task<Product> AddProduct(Product product)
        {
            _products.Add(product);
            return await Task.FromResult(product);
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(_products);
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        public Task DeleteProductById(int Id)
        {
            _products.Remove(_products.FirstOrDefault(p => p.Id == Id));
            return Task.CompletedTask;
        }

        public Task UpdateProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
            }
            return Task.CompletedTask;
        }
        public async Task EventOccured(Product product, string evt)
        {
            _products.SingleOrDefault(p => p.Id == product.Id).Name = $"{product.Name} evt:{evt}";
            await Task.CompletedTask;
        }
    }
}
