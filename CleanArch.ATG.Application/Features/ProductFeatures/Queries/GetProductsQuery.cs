using CleanArch.ATG.Domain.Entities;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Queries
{
    public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
}
