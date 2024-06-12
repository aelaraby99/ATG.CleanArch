using CleanArch.ATG.Application.Features.ProductFeatures.Commands;
using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Data;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand,Product>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Product> Handle( AddProductCommand request , CancellationToken cancellationToken )
        {
            Product product = new Product();
            product.Name = request.product.Name;
            product.Price = request.product.Price;
            var addedProduct = await _unitOfWork.GenericRepository<Product>().AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return addedProduct;
        }
    }
}
