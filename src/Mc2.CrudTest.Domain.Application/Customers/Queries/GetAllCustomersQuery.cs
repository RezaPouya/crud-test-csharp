using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Application.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerOutputDto>>
    {
    }
}
