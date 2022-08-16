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

        Task Create(Customer customer, CancellationToken cancellationToken = default); 

        Task Update(Customer customer, CancellationToken cancellationToken = default);

        Task DeleteAsync(string email, CancellationToken cancellationToken = default); 
    }
}
