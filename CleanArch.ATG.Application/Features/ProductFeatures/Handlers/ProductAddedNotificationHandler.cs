using CleanArch.ATG.Application.Features.ProductFeatures.Notifications;
using CleanArch.ATG.Infrastructure.Data;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Handlers
{
    public class ProductAddedNotificationHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly IFakeDataRepo _fakeDataRepo;

        public ProductAddedNotificationHandler( IFakeDataRepo fakeDataRepo )
        {
            _fakeDataRepo = fakeDataRepo;
        }
        public Task Handle( ProductAddedNotification notification , CancellationToken cancellationToken )
        {
            return _fakeDataRepo.EventOccured(notification.product , "Product Added");
        }
    }
}
