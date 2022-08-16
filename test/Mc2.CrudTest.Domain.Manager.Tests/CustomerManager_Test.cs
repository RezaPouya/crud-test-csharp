using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using Mc2.CrudTest.Domain.Manager.Customers;
using Mc2.CrudTest.Domain.Manager.Tests.DataFixtures;
using System;
using Xunit;

namespace Mc2.CrudTest.Domain.Manager.Tests
{
    public class CustomerManager_Test : IClassFixture<ApplicationDbContextSeedDataFixture>
    {
        private ICustomerManager _manager;

        public CustomerManager_Test(ApplicationDbContextSeedDataFixture fixture) 
        {
            _manager = new CustomerManager(fixture._applicationDbContext);
        }

        [Fact]
        public void should_throw_exception_if_email_is_not_unique()
        {
            // arrange
            var inputDto = new CustomerInputDto()
            {
                Email = "a@b.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+44 1-2222 8888"
            };

            // act
            Action act = () => _manager.CreateAsync(inputDto).GetAwaiter().GetResult();

            // assert
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("There is a customer with same email.", ex.Message);
        }
    }
}