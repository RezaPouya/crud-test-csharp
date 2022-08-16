using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using Mc2.CrudTest.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Domain.Manager.Customers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            if (await IsEmailExistAsync(inputDto.Email))
                throw new CustomerException("There is a customer with same email.");
        }

        public Task DeleteAsync(string email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AsNoTracking().AnyAsync(p => p.Email.Equals(email.Trim()), cancellationToken);
        }
    }
}