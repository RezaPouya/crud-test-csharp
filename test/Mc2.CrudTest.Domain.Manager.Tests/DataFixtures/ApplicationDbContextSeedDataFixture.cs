using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace Mc2.CrudTest.Domain.Manager.Tests.DataFixtures
{
    public class ApplicationDbContextSeedDataFixture : IDisposable
    {
        public ApplicationDbContext _applicationDbContext { get; private set; }


        private Customer customer_1 = new Customer("a@b.com",
            new CustomerPersonalInfo("Mr.A", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+989383810430"),
            "IR0000001");

        private Customer customer_2 = new Customer("a@c.com",
            new CustomerPersonalInfo("Mr.B", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+989383810430"),
            "IR0000001");

        private Customer customer_3 = new Customer("a@d.com",
            new CustomerPersonalInfo("Mr.C", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+989383810430"),
            "IR0000001");

        public ApplicationDbContextSeedDataFixture()
        {
            SetApplicationDbContext();
            _applicationDbContext.Customers.Add(customer_1);
            _applicationDbContext.Customers.Add(customer_2);
            _applicationDbContext.Customers.Add(customer_3);
            _applicationDbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

       
        private void SetApplicationDbContext()
        {
            var dbName = $"AuthorPostsDb_{DateTime.Now.ToFileTimeUtc()}";
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            var mediatorMoq = new Mock<IMediator>();
            _applicationDbContext = new ApplicationDbContext(dbContextOptions , mediatorMoq.Object);
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}