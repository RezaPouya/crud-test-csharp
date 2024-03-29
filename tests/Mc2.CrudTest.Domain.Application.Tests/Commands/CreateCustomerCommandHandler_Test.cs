﻿using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Application.Tests.BasicRecords;
using Mc2.CrudTest.Domain.Application.Tests.Infrastructure;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Mc2.CrudTest.Domain.Application.Tests.Commands
{
    public class CreateCustomerCommandHandler_Test : TestFixture, IDisposable
    {
        public CreateCustomerCommandHandler_Test(CustomWebApplicationFactory factory) : base(factory)
        {
            CreateInitialCustomers();
        }

        [Fact]
        public void should_create_user()
        {
            using var scope = _scopeFactory.CreateScope();

            // Arrange

            var command = new CreateCustomerCommand
            {
                BankAccountNumber = "IR00000001",
                DateOfBirth = DateTime.Now.AddYears(-32),
                Email = "r.pouya.create@hotmail.com",
                FirstName = "Reza-create",
                LastName = "Pouya-create",
                PhoneNumber = "+989383810430"
            };

            // Act
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            mediator.Send(command).GetAwaiter().GetResult();

            // Assert
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var customer = applicationDbContext
                .Customers
                .AsNoTracking()
                .FirstOrDefault(p => p.Email.Equals(command.Email.SanitizeToLower()));

            Assert.NotNull(customer);
        }

        [Fact]
        public void should_throw_exception_if_want_to_insert_duplicate_email()
        {
            using var scope = _scopeFactory.CreateScope();

            var command = new CreateCustomerCommand
            {
                BankAccountNumber = "IR000",
                DateOfBirth = System.DateTime.Now.AddYears(-32),
                Email = TestCustomers.customer_1.Email,
                FirstName = "Reza",
                LastName = "Pouya",
                PhoneNumber = "+989383810430"
            };

            // Act
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            Action act = () => mediator.Send(command, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
            var ex = Assert.Throws<CustomerException>(act);
            var erroMsg = ErrorMessages.GetMessage(ErrorCodes.CustomerErrorCodes.DuplicateEmail);
            Assert.Contains(erroMsg, ex.Description);
        }

        public void Dispose()
        {
            ResetState().GetAwaiter().GetResult();
        }
    }
}