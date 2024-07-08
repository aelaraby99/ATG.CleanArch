using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Commands.DeleteProductCommands
{
    internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.GenericRepository<Product>().DeleteById(request.Id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
