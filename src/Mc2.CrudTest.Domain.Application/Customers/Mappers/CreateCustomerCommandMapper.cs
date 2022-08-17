using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Application.Customers.Mappers
{
    internal static class CreateCustomerCommandMapper
    {
        internal static CustomerInputDto MapToInputDto(this CreateCustomerCommand command)
        {
            return new CustomerInputDto
            {
                BankAccountNumber = command.BankAccountNumber,
                DateOfBirth = command.DateOfBirth,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
            };
        }
    }
}
