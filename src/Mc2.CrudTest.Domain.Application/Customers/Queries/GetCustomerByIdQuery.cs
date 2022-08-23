using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using MediatR;

namespace Mc2.CrudTest.Domain.Application.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerOutputDto>
    {
        public GetCustomerByIdQuery()
        {
        }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}