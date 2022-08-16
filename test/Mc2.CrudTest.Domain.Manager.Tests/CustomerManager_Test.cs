using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Manager.Customers;
using Mc2.CrudTest.Domain.Manager.Tests.DataFixtures;
using Xunit;

namespace Mc2.CrudTest.Domain.Manager.Tests
{
    internal class CustomerManager_Test : IClassFixture<ApplicationDbContextSeedDataFixture>
    {
        private ICustomerManager _manager;

        public CustomerManager_Test(ApplicationDbContextSeedDataFixture fixture)
        {
            _manager = new CustomerManager(fixture._applicationDbContext);
        }
    }
}