using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal static class CustomerMoq
    {
        private static CustomerName _name;
        private static Customer _customer;

        public static Customer GetDefaultCustomer()
        {
            _name = new CustomerName();
            _customer = new Customer(_name);

            return _customer;
        }
    }
}