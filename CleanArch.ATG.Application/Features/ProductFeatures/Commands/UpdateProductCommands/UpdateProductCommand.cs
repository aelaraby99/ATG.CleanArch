using CleanArch.ATG.Domain.Entities;
using MediatR;


namespace CleanArch.ATG.Application.Features.ProductFeatures.Commands.UpdateProductCommands
{
    public record UpdateProductCommand(Product product) : IRequest;
}
