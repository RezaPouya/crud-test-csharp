using Mc2.CrudTest.Domain.Application.Customers.Commands;
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
    public class CreateCustomerCommandHandler_Test : TestFixture
    {
        public CreateCustomerCommandHandler_Test(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public void should_create_user()
        {
            CreateInitialCustomers();
            // Arrange
            var command = new CreateCustomerCommand
            {
                BankAccountNumber = "IR000",
                DateOfBirth = DateTime.Now.AddYears(-32),
                Email = "r.pouya@hotmail.com",
                FirstName = "Reza",
                LastName = "Pouya",
                PhoneNumber = "+989383810430"
            };

            using (var scope = _scopeFactory.CreateScope())
            {
                // Act
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                mediator.Send(command);

                // Assert
                var customer = GetCustomer(command.Email, scope);

                Assert.NotNull(customer);
            }

            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void should_throw_exception_if_want_to_insert_duplicate_email()
        {
            CreateInitialCustomers();

            var command = new CreateCustomerCommand
            {
                BankAccountNumber = "IR000",
                DateOfBirth = System.DateTime.Now.AddYears(-32),
                Email = TestCustomers.customer_1.Email,
                FirstName = "Reza",
                LastName = "Pouya",
                PhoneNumber = "+989383810430"
            };

            using (var scope = _scopeFactory.CreateScope())
            {
                // Act
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                Action act = () => mediator.Send(command, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
                var ex = Assert.Throws<CustomerException>(act);
                Assert.Contains(ErrorMessages.GetMessage(ErrorCodes.CustomerErrorCodes.DuplicateEmail), ex.Message);
            }
            ResetState().GetAwaiter().GetResult();
        }

        private static Customer? GetCustomer(string email, IServiceScope scope)
        {
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return applicationDbContext
                .Customers
                .AsNoTracking()
                .FirstOrDefault(p => p.Email.Equals(email.SanitizeToLower()));
        }
    }
}