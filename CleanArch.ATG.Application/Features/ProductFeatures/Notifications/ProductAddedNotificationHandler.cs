using CleanArch.ATG.Infrastructure.Data;
using MediatR;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Notifications
{
    public class ProductAddedNotificationHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly IFakeDataRepo _fakeDataRepo;

        public ProductAddedNotificationHandler(IFakeDataRepo fakeDataRepo)
        {
            _fakeDataRepo = fakeDataRepo;
        }
        public Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {
            _fakeDataRepo.EventOccured(notification.product, "Product Added");
            //Tasks like sending email, sms, logging etc
            return Task.CompletedTask;
        }
    }
}
