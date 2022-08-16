using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal class CustomerMoqRepository : ICustomerRepository
    {
        private ApplicationDbContext _dbContext;

        public CustomerMoqRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _dbContext.Add(customer);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(customer);
            _dbContext.SaveChanges();
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.Email.Equals(email.Trim()), cancellationToken);
        }

        public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(customer);
            _dbContext.SaveChanges();
        }
    }
}