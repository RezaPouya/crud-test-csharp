using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal static class CustomerMoq
    {
        private static string _email = "rpouya@hotmail.com";
        private static string _bankAccountNumber = "IR0000000000000000000000";
        private static CustomerName _name;
        private static Customer _customer;
        private static CustomerDateOfBirth _dateOfBirth;
        private static CustomerPhoneNumber _phoneNumber;

        public static Customer GetDefaultCustomer()
        {
            _name = new CustomerName("Reza", "Pouya");
            _dateOfBirth = new CustomerDateOfBirth(System.DateTime.Now.AddYears(-32));
            _phoneNumber = new CustomerPhoneNumber("+98 9383810430");
            _customer = new Customer(_email,_name, _dateOfBirth, _phoneNumber, _bankAccountNumber);

            return _customer;
        }


        public static Customer GetDefaultCustomer(string email ="", string bankAccountNumber = "")
        {
            _name = new CustomerName("Reza", "Pouya");
            _dateOfBirth = new CustomerDateOfBirth(System.DateTime.Now.AddYears(-32));
            _phoneNumber = new CustomerPhoneNumber("+98 9383810430");
            _customer = new Customer(email, _name, _dateOfBirth, _phoneNumber, bankAccountNumber);

            return _customer;
        }
    }
}