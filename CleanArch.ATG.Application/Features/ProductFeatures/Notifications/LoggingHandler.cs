using CleanArch.ATG.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Notifications
{
    public class LoggingHandler : INotificationHandler<ProductAddedNotification>
    {
        private readonly IFakeDataRepo _fakeDataRepo;

        public LoggingHandler(IFakeDataRepo fakeDataRepo)
        {
            _fakeDataRepo = fakeDataRepo;
        }
        public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
        {
            await _fakeDataRepo.EventOccured(notification.product, "Product Chached");
        }
    }
}
