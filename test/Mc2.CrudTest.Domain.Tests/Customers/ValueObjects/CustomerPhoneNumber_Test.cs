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


        [Fact]
        public void Should_throw_exception_if_phone_number_is_too_long()
        {
            Action act = () => new CustomerPhoneNumber("+9809383810430654546546421357498734321431313221323233");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer phone number is not valid.", ex.Message);
        }


        [Fact]
        public void Should_throw_exception_if_entered_phone_number_is_not_cellphone_type()
        {
            // https://www.interfax.net/en/help/faxnumber_format
            Action act = () => new CustomerPhoneNumber("+44 1-2222 8888");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("Only cellphone number is acceptable.", ex.Message);
        }
    }
}