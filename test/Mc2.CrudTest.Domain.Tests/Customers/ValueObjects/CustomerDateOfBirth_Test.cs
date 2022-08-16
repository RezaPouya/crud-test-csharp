using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Customers.ValueObjects
{
    public class CustomerDateOfBirth_Test
    {
        private readonly Customer _customer;

        public CustomerDateOfBirth_Test()
        {
            _customer = CustomerMoq.GetDefaultCustomer();
        }

        
    }
}
