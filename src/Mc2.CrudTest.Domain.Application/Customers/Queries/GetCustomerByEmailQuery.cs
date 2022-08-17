using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.Queries
{
    public class GetCustomerByEmailQuery : IRequest<CustomerOutputDto>
    {
        public GetCustomerByEmailQuery()
        {
        }

        public GetCustomerByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}