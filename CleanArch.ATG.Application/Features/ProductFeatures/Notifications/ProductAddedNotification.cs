using CleanArch.ATG.Domain.Entities;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Notifications
{
    public record ProductAddedNotification(Product product) : INotification;
}
