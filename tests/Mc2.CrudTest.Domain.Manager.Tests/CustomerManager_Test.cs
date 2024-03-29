﻿using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using Mc2.CrudTest.Domain.DataAccess;
using Mc2.CrudTest.Domain.Manager.Customers;
using Mc2.CrudTest.Domain.Manager.Tests.DataFixtures;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Mc2.CrudTest.Domain.Manager.Tests
{
    public class CustomerManager_Test : IClassFixture<ApplicationTestFixture>
    {
        private ICustomerManager _manager;
        private IMediator _mediator;
        private ApplicationDbContext _dbContext;

        public CustomerManager_Test(ApplicationTestFixture fixture)
        {
            _dbContext = fixture._applicationDbContext;
            _mediator = fixture._meidator;
            var ilogger = new Mock<ILogger<CustomerManager>>();
            _manager = new CustomerManager(_dbContext, _mediator, ilogger.Object);
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
            Assert.Contains(ErrorMessages.GetMessage(ErrorCodes.CustomerErrorCodes.DuplicateEmail), ex.Description);
        }

        [Fact]
        public void should_throw_exception_if_fname_lname_dateOfBirth_combination_is_not_unique_in_create()
        {
            // arrange
            var inputDto = new CustomerInputDto()
            {
                Email = "a@comibnation_create.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.A",
                LastName = "Programmer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+44 1-2222 8888"
            };

            // act
            Action act = () => _manager.CreateAsync(inputDto).GetAwaiter().GetResult();

            // assert
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains(ErrorMessages.GetMessage(ErrorCodes.CustomerErrorCodes.DuplicateCustomerInfo), ex.Description);
        }

        [Fact]
        public void should_create_customer()
        {
            // arrange
            var inputDto = new CustomerInputDto()
            {
                Email = "a@create.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z_create",
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
                FirstName = "Mr.Z111",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+989383810430"
            };

            var updateInput = new CustomerInputDto()
            {
                Email = "a@b.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z111",
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

        [Fact]
        public void we_should_be_able_to_update_customer()
        {
            // arrange
            var createInput = new CustomerInputDto()
            {
                Email = "a@update2.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Z",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+989383810430"
            };

            var updateInput = new CustomerInputDto()
            {
                Email = createInput.Email,
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Update2",
                LastName = "Developer2",
                DateOfBirth = DateTime.Now.AddYears(-22),
                PhoneNumber = "+989383810430"
            };

            // act
            var createdCustomer = _manager.CreateAsync(createInput).GetAwaiter().GetResult();
            updateInput.Id = createdCustomer.Id;
            var updatedCustomer = _manager.UpdateAsync(updateInput).GetAwaiter().GetResult();

            var customer = _dbContext.Customers.FirstOrDefault(p => p.Email.Equals(createInput.Email));
            // assert
            Assert.NotNull(customer);
            Assert.Equal(updatedCustomer.PersonalInfo.FirstName, customer.PersonalInfo.FirstName);
            Assert.Equal(updatedCustomer.PersonalInfo.LastName, customer.PersonalInfo.LastName);
            Assert.Equal(updatedCustomer.PersonalInfo.DateOfBirth.Date, customer.PersonalInfo.DateOfBirth.Date);
        }

        [Fact]
        public void should_throw_exception_if_customer_not_found_in_delete()
        {
            Action act = () => _manager.DeleteAsync(3452345 , default(CancellationToken)).GetAwaiter().GetResult();

            // assert
            var ex = Assert.Throws<CustomerException>(act);
            Assert.Contains("There is no customer with this email.", ex.Message);
        }

        [Fact]
        public void should_be_able_to_delete_customer()
        {
            // arrange
            var createInput = new CustomerInputDto()
            {
                Email = "a@delete2.com",
                BankAccountNumber = "IR0000001",
                FirstName = "Mr.Zdelete2",
                LastName = "Developer",
                DateOfBirth = DateTime.Now.AddYears(-20),
                PhoneNumber = "+989383810430"
            };

            // act
            var customer = _manager.CreateAsync(createInput).GetAwaiter().GetResult();
            var customerId = customer.Id;

            _manager.DeleteAsync(customerId).GetAwaiter().GetResult();

            var customer2 = _dbContext.Customers.FirstOrDefault(p => p.Id == customerId);

            // assert
            Assert.Null(customer2);
        }
    }
}