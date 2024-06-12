using CleanArch.ATG.Application.Features.ProductFeatures.Queries;
using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using MediatR;


namespace CleanArch.ATG.Application.Features.ProductFeatures.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery , Product>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdHandler( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Product> Handle( GetProductByIdQuery request , CancellationToken cancellationToken )
        {
            return await _unitOfWork.GenericRepository<Product>().GetByIdAsync(request.id);
        }
    }
}
