using Mc2.CrudTest.Domain.Customers.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Domain.Application.EventHandlers.Customers
{
    public class CustomerUpdatedEtoHandler : INotificationHandler<CustomerUpdatedEto>
    {
        private readonly ILogger<CustomerUpdatedEtoHandler> _logger;

        public CustomerUpdatedEtoHandler(ILogger<CustomerUpdatedEtoHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerUpdatedEto notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}