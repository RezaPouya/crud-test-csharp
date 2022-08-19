using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace Mc2.CrudTest.Domain.Manager.Tests.DataFixtures
{
    public class ApplicationTestFixture : IDisposable
    {
        public ApplicationDbContext _applicationDbContext { get; private set; }
        public IMediator _meidator; 

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

        public ApplicationTestFixture()
        {
            SetServices();
            _applicationDbContext.Customers.Add(customer_1);
            _applicationDbContext.Customers.Add(customer_2);
            _applicationDbContext.Customers.Add(customer_3);
            _applicationDbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

       
        private void SetServices()
        {
            var dbName = $"CrudTestDb_{DateTime.Now.ToFileTimeUtc()}";
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            _meidator = new Mock<IMediator>().Object;
            _applicationDbContext = new ApplicationDbContext(dbContextOptions , _meidator);
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}