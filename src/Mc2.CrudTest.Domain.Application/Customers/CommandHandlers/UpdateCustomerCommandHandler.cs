using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Application.Customers.Mappers;
using Mc2.CrudTest.Domain.Customers;
using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.CommandHandlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerManager _manager;

        public UpdateCustomerCommandHandler(ICustomerManager manager)

        {
            _manager = manager;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _manager.UpdateAsync(request.MapToInputDto(), cancellationToken);
            return new Unit();
        }
    }
}