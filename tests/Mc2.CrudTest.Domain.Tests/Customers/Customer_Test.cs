using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.Events;
using Mc2.CrudTest.Domain.Helpers;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using System;
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
        public void customer_should_have_an_event_after_creation()
        {
            Assert.NotEmpty(_customer.GetEvents());
        }

        [Fact]
        public void customer_should_have_an_event_after_creation_and_event_should_have_all_customer_data()
        {
            var events = _customer.GetEvents();
            var firstEvent = (CustomerCreatedEto)events[0];
            Assert.NotNull(firstEvent);
            Assert.Equal(_customer.Email, firstEvent.Email);
        }

        [Fact]
        public void CustomerShouldHave_personal_info()
        {
            Assert.NotNull(_customer.PersonalInfo);
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
            Action act = () => CustomerMoq.GetDefaultCustomer("");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer email cannot be empty.", ex.Message);
        }

        [Fact]
        public void should_throw_exception_if_email_length_is_to_long_gt_254()
        {
            var str = StringGeneratorHelper.GenerateRandom(255);
            Action act = () => CustomerMoq.GetDefaultCustomer(str, "IR");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The email is too long and not valid.", ex.Message);
        }

        [Fact]
        public void should_throw_exception_if_email_is_not_valid()
        {
            Action act = () => CustomerMoq.GetDefaultCustomer("asdfasdfadf.asdfasdf", "IR");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer email is not valid.", ex.Message);
        }

        [Fact]
        public void should_have_bank_account_number()
        {
            Assert.NotNull(_customer.BankAccountNumber);
        }

        [Fact]
        public void should_throw_exception_if_bank_account_number_is_empty()
        {
            Action act = () => CustomerMoq.GetDefaultCustomer("rpouya@hotmail.com");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer bank account number cannot be empty.", ex.Message);
        }

        [Fact]
        public void should_throw_exception_if_bank_account_number_length_is_to_long_gt_34()
        {
            var str = StringGeneratorHelper.GenerateRandom(36);
            Action act = () => CustomerMoq.GetDefaultCustomer("rpouya@hotmail.com", str);
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The bank account number is too long and not valid.", ex.Message);
        }

        [Fact]
        public void should_be_able_to_update_customer_info()
        {
            _customer.Update("a.pouya@a.com" , "Ahmad", "Pouya", DateTime.Now.AddYears(-35), "IR455555", "+989163737500");

            Assert.Equal("a.pouya@a.com", _customer.Email);
            Assert.Equal("ahmad", _customer.PersonalInfo.FirstName);
            Assert.Equal("pouya", _customer.PersonalInfo.LastName);
            Assert.Equal(DateTime.Now.AddYears(-35).Date, _customer.PersonalInfo.DateOfBirth.Date);
            Assert.Equal("ir455555", _customer.BankAccountNumber);
            Assert.Equal("989163737500", _customer.PhoneNumber.Number.ToString());
        }


        [Fact]
        public void should_update_event_have_info()
        {
            _customer.Update("a.pouya@a2.com", "Ahmad", "Pouya2", DateTime.Now.AddYears(-35), "IR455555", "+989163737500");
            var events = _customer.GetEvents();
            var firstEvent = (CustomerUpdatedEto)events[1];
            Assert.Equal("a.pouya@a2.com", firstEvent.Email);
            Assert.Equal("ahmad", firstEvent.FirstName);
            Assert.Equal("pouya2", firstEvent.LastName);
            Assert.Equal(DateTime.Now.AddYears(-35).Date, firstEvent.DateOfBirth.Date);
            Assert.Equal("ir455555", firstEvent.BankAccountNumber);
            Assert.Equal("989163737500".Trim(), firstEvent.PhoneNumber.ToString());
        }
    }
}