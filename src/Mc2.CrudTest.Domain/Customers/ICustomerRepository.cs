using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers
{
    public interface ICustomerRepository 
    {
        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task CreateAsync(Customer customer, CancellationToken cancellationToken = default); 

        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

        Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default); 
    }
}
