using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public DeleteCustomerCommand()
        {
        }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}