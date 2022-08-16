using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using System;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Customers.ValueObjects
{
    public class CustomerPhoneNumber_Test
    {
        private readonly Customer _customer;

        public CustomerPhoneNumber_Test()
        {
            _customer = CustomerMoq.GetDefaultCustomer();
        }

        [Fact]
        public void Should_have_number_property()
        {
            Assert.NotNull(_customer.PhoneNumber.Number);
        }

        [Fact]
        public void Should_throw_exception_if_phone_number_is_empty()
        {
            Action act = () => new CustomerPhoneNumber("");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer phone number cannot be empty.", ex.Message);
        }
    }
}