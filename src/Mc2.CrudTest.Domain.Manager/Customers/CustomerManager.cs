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

        public async Task<Customer> CreateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            if (await IsEmailExistAsync(inputDto.Email))
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.DuplicateEmail);

            if (await IsPersonalInfoUniqueAsync(inputDto, cancellationToken))
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.DuplicateCustomerInfo);

            var customerPhoneNumber = new CustomerPhoneNumber(inputDto.PhoneNumber.Trim());

            Customer customer = new Customer(inputDto.Email.Trim(),
                CreateCustomerPersonalInfo(inputDto)
                , customerPhoneNumber,
                inputDto.BankAccountNumber);

            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }

        public async Task<Customer> UpdateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            if (inputDto == null)
                throw new ArgumentNullException(nameof(inputDto));

            if (await IsPersonalInfoUniqueAsync(inputDto, cancellationToken))
                throw new CustomerException("There is a customer with same personal info.");

            Customer customer = await GetCustomerAsync(inputDto.Id, cancellationToken);

            if (customer is null)
                throw new CustomerException("There is no customer with this email.");

            if (customer.Email.Equals(inputDto.Email.SanitizeToLower()) == false)
            {
                if (await IsEmailExistAsync(inputDto.Email))
                    throw new CustomerException("There is a customer with same email.");
            }

            customer.Update(inputDto.Email ,
                inputDto.FirstName,
                inputDto.LastName,
                inputDto.DateOfBirth,
                inputDto.BankAccountNumber,
                inputDto.PhoneNumber);

            _dbContext.Update(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id < 0)
                throw new CustomerException("Id is not valid");

            Customer customer = await GetCustomerAsync(id, cancellationToken);

            if (customer is null)
                throw new CustomerException("There is no customer with this email.");

            _dbContext.Remove(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediatR.Publish(new CustomerDeletedEto(id));
        }

        private CustomerPersonalInfo CreateCustomerPersonalInfo(CustomerInputDto inputDto)
        {
            return new CustomerPersonalInfo(inputDto.FirstName.SanitizeToLower(),
                inputDto.LastName.SanitizeToLower(),
                inputDto.DateOfBirth);
        }

        #region queries

        private async Task<bool> IsPersonalInfoUniqueAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AsNoTracking()
                .Where(p => p.Id != inputDto.Id)
                .Where(p => p.PersonalInfo.FirstName.Equals(inputDto.FirstName.SanitizeToLower()))
                .Where(p => p.PersonalInfo.LastName.Equals(inputDto.LastName.SanitizeToLower()))
                .Where(p => p.PersonalInfo.DateOfBirth.Date.Equals(inputDto.DateOfBirth.Date))
                .AnyAsync(cancellationToken);
        }

        private async Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AsNoTracking().AnyAsync(p => p.Email.Equals(email.SanitizeToLower()), cancellationToken);
        }

        private async Task<Customer?> GetCustomerAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(p => p.Email.Equals(email.SanitizeToLower()), cancellationToken);
        }

        private async Task<Customer?> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        #endregion queries
    }
}