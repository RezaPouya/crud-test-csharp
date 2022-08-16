using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Tests.MoqObjects;
using Mc2.CrudTest.Utility.Helpers;
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
            Action act = () => CustomerMoq.GetDefaultCustomer("");
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The customer email cannot be empty.", ex.Message);
        }

        [Fact]
        public void should_throw_exception_if_email_length_is_to_long_gt_254()
        {
            var str = StringGeneratorHelper.GenerateRandom(255);
            Action act = () => CustomerMoq.GetDefaultCustomer(str , "IR");
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
            Action act = () => CustomerMoq.GetDefaultCustomer( "rpouya@hotmail.com",str);
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("The bank account number is too long and not valid.", ex.Message);
        }

    }
}