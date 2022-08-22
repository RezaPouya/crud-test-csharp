using Mc2.CrudTest.Domain.Application.Tests.BasicRecords;
using Mc2.CrudTest.Domain.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Application.Tests.Infrastructure
{
    public class TestFixture : IClassFixture<CustomWebApplicationFactory>
    {
        public TestFixture(CustomWebApplicationFactory factory)
        {
            _factory = factory;

            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            _configuration = _factory.Services.GetRequiredService<IConfiguration>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new Respawn.Graph.Table[] {
                    new Respawn.Graph.Table("__EFMigrationsHistory")
                }
            };

        }

        protected IServiceScopeFactory _scopeFactory = null!;
        protected readonly CustomWebApplicationFactory _factory;
        protected readonly IConfiguration _configuration;
        protected readonly Checkpoint _checkpoint;

        public async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected void CreateInitialCustomers()
        {
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = _scopeFactory.CreateScope();

            ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (applicationDbContext.Customers.Any(p => p.Email == TestCustomers.customer_1.Email) == false)
            {
                applicationDbContext.Customers.Add(TestCustomers.customer_1);
            }

            applicationDbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}