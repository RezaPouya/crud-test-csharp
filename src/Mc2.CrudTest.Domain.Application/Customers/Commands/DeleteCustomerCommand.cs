using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public DeleteCustomerCommand()
        {

        }

        public DeleteCustomerCommand(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}