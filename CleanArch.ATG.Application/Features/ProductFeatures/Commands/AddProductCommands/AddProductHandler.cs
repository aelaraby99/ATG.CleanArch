using AutoMapper;
using CleanArch.ATG.Application.Features.ProductFeatures.Commands.AddProductCommands;
using CleanArch.ATG.Application.Interfaces;
using CleanArch.ATG.Domain.Entities;
using CleanArch.ATG.Infrastructure.Data;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand,Product>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddProductHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Product> Handle( AddProductCommand request , CancellationToken cancellationToken )
        {
            var product = _mapper.Map<Product>(request);
            var addedProduct = await _unitOfWork.GenericRepository<Product>().AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return addedProduct;
        }
    }
}
