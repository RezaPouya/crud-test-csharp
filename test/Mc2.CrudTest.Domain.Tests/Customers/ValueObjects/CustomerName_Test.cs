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
    public class CustomerName_Test
    {
        private readonly Customer _customer;
        public CustomerName_Test()
        {
            _customer = CustomerMoq.GetDefaultCustomer();
        }

        [Fact]
        public void Should_have_fname_and_lname_property()
        {
            Assert.Null(_customer.Name.FirstName);
            Assert.Null(_customer.Name.LastName);
        }

        [Fact]
        public void Should_throw_Exception_if_customer_fname_is_empty()
        {
            Action act = () => new CustomerName("", "Pouya");
            Assert.Throws<CustomerException>(act);
        }
    }
}
