using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using System;
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
            Assert.NotNull(_customer.Name.FirstName);
            Assert.NotNull(_customer.Name.LastName);
        }

        [Fact]
        public void Should_throw_exception_if_customer_fname_is_empty()
        {
            Action act = () => new CustomerName("", "Pouya");
            Assert.Throws<CustomerException>(act);
        }

        [Fact]
        public void Should_throw_exception_if_fname_length_is_less_than_3_characters()
        {
            Action act = () => new CustomerName("R", "Pouya");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer first name should have more than 2 characters.", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_if_fname_length_is_greater_than_64_characters()
        {
            Action act = () => new CustomerName("Rqwerqwerqwersdfasdflksjdflasjdflaskjdfl;asdjf;alsdfj;asldfjasldkfja;sdf", "Pouya");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer first name Length should be less than 65 characters.", ex.Message);
        }

        //[Fact]
        //public void Should_throw_exception_if_lname_length_is_less_than_3_characters()
        //{
        //    Action act = () => new CustomerName("Reza", "P");
        //    var ex = Assert.Throws<CustomerException>(act);
        //    Assert.Contains("The customer last name should have more than 2 characters.", ex.Message);
        //}

        //[Fact]
        //public void Should_throw_exception_if_lname_length_is_greater_than_64_characters()
        //{
        //    Action act = () => new CustomerName("Reza", "qwerqwerqwersdfasdflksjdflasjdflaskjdfl;asdjf;alsdfj;asldfjasldkfja;sdf");
        //    var ex = Assert.Throws<CustomerException>(act);
        //    Assert.Contains("The customer last name Length should be less than 65 characters.", ex.Message);
        //}
    }
}