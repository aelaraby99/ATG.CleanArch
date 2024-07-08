using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Queries
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }
    }
}
