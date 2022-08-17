using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;
using Mc2.CrudTest.Domain.Customers.Events;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Domain.Manager.Customers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediatR;

        public CustomerManager(ApplicationDbContext dbContext, IMediator mediatR)
        {
            _dbContext = dbContext;
            _mediatR = mediatR;
        }

        public async Task CreateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            if (await IsEmailExistAsync(inputDto.Email))
                throw new CustomerException("There is a customer with same email.");

            if (await IsPersonalInfoUniqueAsync(inputDto, cancellationToken))
                throw new CustomerException("There is a customer with same personal info.");

            var customerPhoneNumber = new CustomerPhoneNumber(inputDto.PhoneNumber.Trim());

            Customer customer = new Customer(inputDto.Email.Trim(),
                CreateCustomerPersonalInfo(inputDto)
                , customerPhoneNumber,
                inputDto.BankAccountNumber);

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            if (await IsPersonalInfoUniqueAsync(inputDto, cancellationToken))
                throw new CustomerException("There is a customer with same personal info.");

            Customer customer = await GetCustomerAsync(inputDto.Email, cancellationToken);

            if (customer is null)
                throw new CustomerException("There is no customer with this email.");

            customer.Update(inputDto.FirstName, inputDto.LastName, inputDto.DateOfBirth, inputDto.BankAccountNumber, inputDto.PhoneNumber);
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            Customer customer = await GetCustomerAsync(email, cancellationToken);

            if (customer is null)
                throw new CustomerException("There is no customer with this email.");

            _dbContext.Remove(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _mediatR.Publish(new CustomerDeletedEto(email.Trim()));
        }

        private CustomerPersonalInfo CreateCustomerPersonalInfo(CustomerInputDto inputDto)
        {
            return new CustomerPersonalInfo(inputDto.FirstName.Trim(), inputDto.LastName.Trim(), inputDto.DateOfBirth);
        }

        #region queries

        private async Task<bool> IsPersonalInfoUniqueAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AsNoTracking()
                .Where(p => !p.Email.Equals(inputDto.Email.Trim()))
                .Where(p => p.PersonalInfo.FirstName.Equals(inputDto.FirstName.Trim()))
                .Where(p => p.PersonalInfo.LastName.Equals(inputDto.LastName.Trim()))
                .Where(p => p.PersonalInfo.DateOfBirth.Date.Equals(inputDto.DateOfBirth.Date))
                .AnyAsync(cancellationToken);
        }

        private async Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AsNoTracking().AnyAsync(p => p.Email.Equals(email.Trim()), cancellationToken);
        }

        private async Task<Customer> GetCustomerAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(p => p.Email.Equals(email.Trim()), cancellationToken);
        }

        #endregion queries
    }
}