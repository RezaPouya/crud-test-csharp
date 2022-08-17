using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Application.Customers.Mappers;
using Mc2.CrudTest.Domain.Customers;
using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerManager _manager;

        public CreateCustomerCommandHandler(ICustomerManager manager)

        {
            _manager = manager;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _manager.CreateAsync(request.MapToInputDto() , cancellationToken);
            return new Unit();
        }
    }
}