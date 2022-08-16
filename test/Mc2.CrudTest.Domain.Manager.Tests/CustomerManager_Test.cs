using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using Mc2.CrudTest.Domain.DataAccess;
using Mc2.CrudTest.Domain.Manager.Customers;
using Mc2.CrudTest.Domain.Manager.Tests.DataFixtures;
using System;
using System.Linq;
using Xunit;

namespace Mc2.CrudTest.Domain.Manager.Tests
{
    public class CustomerManager_Test : IClassFixture<ApplicationDbContextSeedDataFixture>
    {
        private ICustomerManager _manager;
        private ApplicationDbContext _dbContext;
        public CustomerManager_Test(ApplicationDbContextSeedDataFixture fixture)
        {
            _dbContext = fixture._applicationDbContext;
            _manager = new CustomerManager(_dbContext);
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

        [Fact]
        public void should_create_customer()
        {
            // arrange
            var inputDto = new CustomerInputDto()
            {
                Email = "a@create.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+989383810430"
            };

            // act
            _manager.CreateAsync(inputDto).GetAwaiter().GetResult();

            var customer = _dbContext.Customers.FirstOrDefault(p => p.Email.Equals(inputDto.Email));

            // assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void should_throw_exception_if_fname_lname_dateOfBirth_combination_is_not_unique_in_update()
        {
            // arrange
            var createInput = new CustomerInputDto()
            {
                Email = "a@update.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+989383810430"
            };

            var updateInput = new CustomerInputDto()
            {
                Email = "a@b.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+989383810430"
            };

            // act
            _manager.CreateAsync(createInput).GetAwaiter().GetResult();
            Action act = () => _manager.UpdateAsync(updateInput).GetAwaiter().GetResult();

            // assert
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("There is a customer with same personal info.", ex.Message);
        }
    }
}