using Mc2.CrudTest.Domain.Customers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.Mappers
{
    public static class CustomerEtoMapper
    {
        public static CustomerCreatedEto MapToCreateEto(this Customer customer)
        {
            return new CustomerCreatedEto
            {
                BankAccountNumber = customer.BankAccountNumber,
                DateOfBirth = customer.PersonalInfo.DateOfBirth,
                Email = customer.Email,
                FirstName = customer.PersonalInfo.FirstName,
                LastName = customer.PersonalInfo.LastName,
                PhoneNumber = customer.PhoneNumber.Number
            };
        }

        public static CustomerUpdatedEto MapToUpdateEto(this Customer customer)
        {
            return new CustomerUpdatedEto
            {
                Id = customer.Id,
                BankAccountNumber = customer.BankAccountNumber,
                DateOfBirth = customer.PersonalInfo.DateOfBirth,
                Email = customer.Email,
                FirstName = customer.PersonalInfo.FirstName,
                LastName = customer.PersonalInfo.LastName,
                PhoneNumber = customer.PhoneNumber.Number
            };
        }
    }
}
