using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;

namespace Mc2.CrudTest.Domain.Customers
{
    public interface ICustomerManager
    {
        Task<Customer> CreateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default);

        Task<Customer> UpdateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}