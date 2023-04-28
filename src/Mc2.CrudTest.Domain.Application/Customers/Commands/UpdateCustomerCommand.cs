using Mc2.CrudTest.Domain.JsonConverters;
using MediatR;
using System.Text.Json.Serialization;

namespace Mc2.CrudTest.Domain.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        [JsonConverter(typeof(DateOfBirthJsonConverter))]
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}