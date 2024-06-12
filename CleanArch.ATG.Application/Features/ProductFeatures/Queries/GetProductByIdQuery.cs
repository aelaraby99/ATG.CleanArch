using CleanArch.ATG.Domain.Entities;
using MediatR;


namespace CleanArch.ATG.Application.Features.ProductFeatures.Queries
{
    public record GetProductByIdQuery( int id ) : IRequest<Product>;
}
