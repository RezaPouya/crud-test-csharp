using Mc2.CrudTest.Domain.Customers.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Domain.Application.EventHandlers.Customers
{
    public class CustomerCreatedEtoHandler : INotificationHandler<CustomerCreatedEto>
    {
        private readonly ILogger<CustomerCreatedEtoHandler> _logger;

        public CustomerCreatedEtoHandler(ILogger<CustomerCreatedEtoHandler> logger)
        {
            this._logger = logger;
        }

        public Task Handle(CustomerCreatedEto notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}