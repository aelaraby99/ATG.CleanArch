using CleanArch.ATG.Domain.Entities;
using MediatR;


namespace CleanArch.ATG.Application.Features.ProductFeatures.Commands
{
    public record UpdateProductCommand( Product product ) : IRequest;
}
