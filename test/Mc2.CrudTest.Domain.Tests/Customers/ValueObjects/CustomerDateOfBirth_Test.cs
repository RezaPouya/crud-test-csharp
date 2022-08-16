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

        [Fact]
        public void CustomerShouldDateOfBirth()
        {
            Assert.NotNull(_customer.DateOfBirth);
        }


        [Fact]
        public void Should_throw_exception_if_dateofbirth_not_valid()
        {
            Action act = () => new CustomerDateOfBirth(default(DateTime));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The date of birth is not valid.", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_if_dateofbirth_is_set_to_future()
        {
            Action act = () => new CustomerDateOfBirth(DateTime.Now.AddDays(1));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The date of birth cannot set to future.", ex.Message);
        }


        [Fact]
        public void Should_throw_exception_if_dateofbirth_is_set_to_distance_past()
        {
            Action act = () => new CustomerDateOfBirth(DateTime.Now.AddYears(-150));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The date of birth is not valid.", ex.Message);
        }
    }
}
