using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Tests.DataFixtures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal class CustomerMoqRepository : ICustomerRepository, IClassFixture<ApplicationDbContextSeedDataFixture>
    {
        private ApplicationDbContextSeedDataFixture _fixture;

        public CustomerMoqRepository(ApplicationDbContextSeedDataFixture fixture)
        {
            this._fixture = fixture;
        }

        public async Task CreateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _fixture._applicationDbContext.Add(customer);
            _fixture._applicationDbContext.SaveChanges();
        }

        public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _fixture._applicationDbContext.Remove(customer);
            _fixture._applicationDbContext.SaveChanges();
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _fixture._applicationDbContext.Customers.ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return await _fixture._applicationDbContext.Customers.FirstOrDefaultAsync(c => c.Email.Equals(email.Trim()), cancellationToken);
        }

        public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _fixture._applicationDbContext.Update(customer);
            _fixture._applicationDbContext.SaveChanges();
        }
    }
}