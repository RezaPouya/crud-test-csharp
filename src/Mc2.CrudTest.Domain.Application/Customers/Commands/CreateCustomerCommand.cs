using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest
    {
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}