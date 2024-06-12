using CleanArch.ATG.Application.Features.ProductFeatures.Commands;
using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Data;
using MediatR;


namespace CleanArch.ATG.Application.Features.ProductFeatures.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductHandler( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle( UpdateProductCommand request , CancellationToken cancellationToken )
        {
            var existingProduct = await _unitOfWork.GenericRepository<Product>().GetByIdAsync(request.product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = request.product.Name;
                existingProduct.Price = request.product.Price;
                existingProduct.IsDeleted = request.product.IsDeleted;
                await _unitOfWork.GenericRepository<Product>().Update(existingProduct);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
