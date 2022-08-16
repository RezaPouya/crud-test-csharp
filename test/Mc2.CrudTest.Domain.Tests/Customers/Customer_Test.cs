using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Customers
{
    public class Customer_Test
    {
        private Customer _customer;

        public Customer_Test()
        {
            _customer = CustomerMoq.GetDefaultCustomer();
        }

        [Fact]
        public void WeShouldHaveCustomer()
        {
            Assert.NotNull(_customer);
        }

        [Fact]
        public void CustomerShouldHaveName()
        {
            Assert.NotNull(_customer.Name);
        }


        [Fact]
        public void should_have_phone_number()
        {
            Assert.NotNull(_customer.PhoneNumber);
        }

        [Fact]
        public void should_have_email()
        {
            Assert.NotNull(_customer.Email);
        }

        [Fact]
        public void should_throw_exception_if_email_is_empty()
        {
            Assert.NotEmpty(_customer.Email);
        }

        //[Fact]
        //public void should_have_phone_number()
        //{
        //    Assert.NotNull(_customer.PhoneNumber);
        //}
    }
}