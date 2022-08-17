using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;

namespace Mc2.CrudTest.Domain.Application.Customers.Mappers
{
    internal static class CustomerCommandMapper
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

        internal static CustomerInputDto MapToInputDto(this UpdateCustomerCommand command)
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