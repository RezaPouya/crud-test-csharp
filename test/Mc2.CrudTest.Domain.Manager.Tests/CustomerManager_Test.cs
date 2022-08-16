using Mc2.CrudTest.Domain.Manager.Tests.DataFixtures;
using Xunit;

namespace Mc2.CrudTest.Domain.Manager.Tests
{
    internal class CustomerManager_Test : IClassFixture<ApplicationDbContextSeedDataFixture>
    {
        private ApplicationDbContextSeedDataFixture _fixture;

        public CustomerManager_Test(ApplicationDbContextSeedDataFixture fixture)
        {
            _fixture = fixture;
        }
    }
}