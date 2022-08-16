using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.DataAccess;
using System;

namespace Mc2.CrudTest.Domain.Manager.Tests.DataFixtures
{
    internal class ApplicationDbContextSeedDataFixture : IDisposable
    {
        private Customer customer_1 = new Customer("a@b.com",
            new CustomerPersonalInfo("Mr.A", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+983810430"),
            "IR0000001");

        private Customer customer_2 = new Customer("a@c.com",
            new CustomerPersonalInfo("Mr.B", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+983810430"),
            "IR0000001");

        private Customer customer_3 = new Customer("a@d.com",
            new CustomerPersonalInfo("Mr.C", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+983810430"),
            "IR0000001");

        public ApplicationDbContextSeedDataFixture()
        {
            _applicationDbContext.Customers.Add(customer_1);
            _applicationDbContext.Customers.Add(customer_2);
            _applicationDbContext.Customers.Add(customer_3);
            _applicationDbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        internal ApplicationDbContext _applicationDbContext { get; private set; } = new ApplicationDbContext();

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}