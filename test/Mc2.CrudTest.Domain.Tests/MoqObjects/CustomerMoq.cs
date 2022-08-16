using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal static class CustomerMoq
    {
        private static CustomerName _name;
        private static Customer _customer;
        private static CustomerDateOfBirth _dateOfBirth;
        private static CustomerPhoneNumber _phoneNumber;

        public static Customer GetDefaultCustomer()
        {
            _name = new CustomerName("Reza", "Pouya");
            _dateOfBirth = new CustomerDateOfBirth(System.DateTime.Now.AddYears(-32));
            _phoneNumber = new CustomerPhoneNumber();
            _customer = new Customer(_name, _dateOfBirth, _phoneNumber);

            return _customer;
        }
    }
}