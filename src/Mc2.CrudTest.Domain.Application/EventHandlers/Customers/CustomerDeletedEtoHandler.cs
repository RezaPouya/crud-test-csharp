using Mc2.CrudTest.Domain.Customers.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Domain.Application.EventHandlers.Customers
{
    public class CustomerDeletedEtoHandler : INotificationHandler<CustomerDeletedEto>
    {
        private readonly ILogger<CustomerDeletedEtoHandler> _logger;

        public CustomerDeletedEtoHandler(ILogger<CustomerDeletedEtoHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerDeletedEto notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}