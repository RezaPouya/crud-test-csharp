using Mc2.CrudTest.Domain.Customers.DTOs.InputDtos;

namespace Mc2.CrudTest.Domain.Customers
{
    public interface ICustomerManager
    {
        Task CreateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default);

        Task UpdateAsync(CustomerInputDto inputDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(string email, CancellationToken cancellationToken = default);
    }
}