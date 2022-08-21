using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Application.Customers.Mappers
{
    internal static class CustomerQueryMapper
    {
        public static CustomerOutputDto MapToOutputDto(Customer p)
        {
            if (p is null)
                return null; 

            return new CustomerOutputDto
            {
                BankAccountNumber = p.BankAccountNumber,
                DateOfBirth = p.PersonalInfo.DateOfBirth,
                Email = p.Email,
                FirstName = p.PersonalInfo.FirstName,
                LastName = p.PersonalInfo.LastName,
                PhoneNumber = p.PhoneNumber.Number
            };
        }
    }
}
