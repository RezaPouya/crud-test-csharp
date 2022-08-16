using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal static class CustomerMoq
    {
        private static CustomerName _name;
        private static Customer _customer;
        private static CustomerDateOfBirth _dateOfBirth;

        public static Customer GetDefaultCustomer()
        {
            _name = new CustomerName("Reza","Pouya");
            _dateOfBirth = new CustomerDateOfBirth(System.DateTime.Now.AddYears(-32));
            _customer = new Customer(_name , _dateOfBirth);

            return _customer;
        }
    }
}