using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Customers
{
    public class Customer_Test
    {
        private CustomerName _name;
        private Customer _customer;

        public Customer_Test()
        {
            _name = new CustomerName();
            _customer = new Customer(_name);
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
    }
}