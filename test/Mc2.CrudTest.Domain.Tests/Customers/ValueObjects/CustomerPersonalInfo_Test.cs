using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using System;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Customers.ValueObjects
{
    public class CustomerPersonalInfo_Test
    {
        private readonly Customer _customer;

        public CustomerPersonalInfo_Test()
        {
            _customer = CustomerMoq.GetDefaultCustomer();
        }

        [Fact]
        public void Should_have_fname_and_lname_property()
        {
            Assert.NotNull(_customer.PersonalInfo.FirstName);
            Assert.NotNull(_customer.PersonalInfo.LastName);
        }

        [Fact]
        public void Should_throw_exception_if_customer_fname_is_empty()
        {
            Action act = () => new CustomerPersonalInfo("", "Pouya", DateTime.Now.AddYears(-32));
            Assert.Throws<CustomerException>(act);
        }

        [Fact]
        public void Should_throw_exception_if_fname_length_is_less_than_3_characters()
        {
            Action act = () => new CustomerPersonalInfo("R", "Pouya", DateTime.Now.AddYears(-32));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer first name should have more than 2 characters.", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_if_fname_length_is_greater_than_64_characters()
        {
            Action act = () => new CustomerPersonalInfo("Rqwerqwerqwersdfasdflksjdflasjdflaskjdfl;asdjf;alsdfj;asldfjasldkfja;sdf", "Pouya",  DateTime.Now.AddYears(-32));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer first name Length should be less than 65 characters.", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_if_lname_length_is_less_than_3_characters()
        {
            Action act = () => new CustomerPersonalInfo("Reza", "P", DateTime.Now.AddYears(-32));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer last name should have more than 2 characters.", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_if_lname_length_is_greater_than_64_characters()
        {
            Action act = () => new CustomerPersonalInfo("Reza", "qwerqwerqwersdfasdflksjdflasjdflaskjdfl;asdjf;alsdfj;asldfjasldkfja;sdf", DateTime.Now.AddYears(-32));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer last name Length should be less than 65 characters.", ex.Message);
        }


        [Fact]
        public void CustomerShouldDateOfBirth()
        {
            Assert.NotNull(_customer.PersonalInfo.DateOfBirth);
        }


        [Fact]
        public void Should_throw_exception_if_dateofbirth_not_valid()
        {
            Action act = () => new CustomerPersonalInfo("Reza", "Pouya", default); 
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The date of birth is not valid.", ex.Message);
        }

        [Fact]
        public void Should_throw_exception_if_dateofbirth_is_set_to_future()
        {
            Action act = () => new CustomerPersonalInfo("Reza", "Pouya", DateTime.Now.AddDays(1));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The date of birth cannot set to future.", ex.Message);
        }


        [Fact]
        public void Should_throw_exception_if_dateofbirth_is_set_to_distance_past()
        {
            Action act = () => new CustomerPersonalInfo("Reza", "Pouya", DateTime.Now.AddYears(-150));
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The date of birth is not valid.", ex.Message);
        }
    }
}