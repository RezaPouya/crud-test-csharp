using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.CommandHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerManager _manager;

        public DeleteCustomerCommandHandler(ICustomerManager manager)

        {
            _manager = manager;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _manager.DeleteAsync(request.Id, cancellationToken);
            return new Unit();
        }
    }
}