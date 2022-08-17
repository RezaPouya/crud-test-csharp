using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public string Email { get; set; }
    }
}