using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using Mc2.CrudTest.Domain.DataAccess;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Domain.Manager.Customers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CustomerManager> _logger;

        public CustomerManager(ApplicationDbContext dbContext,
            ILogger<CustomerManager> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task CreateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}