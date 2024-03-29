﻿using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;

namespace Mc2.CrudTest.Domain.Tests.MoqObjects
{
    internal static class CustomerMoq
    {
        private static string _email = "rpouya@hotmail.com";
        private static string _bankAccountNumber = "IR0000000000000000000000";
        private static CustomerPersonalInfo _personalInfo;
        private static Customer _customer;
        private static CustomerPhoneNumber _phoneNumber;

        public static Customer GetDefaultCustomer()
        {
            _personalInfo = new CustomerPersonalInfo("Reza", "Pouya" , System.DateTime.Now.AddYears(-32));
            _phoneNumber = new CustomerPhoneNumber("+98 9383810430");
            _customer = new Customer(_email,_personalInfo, _phoneNumber, _bankAccountNumber);

            return _customer;
        }


        public static Customer GetDefaultCustomer(string email ="", string bankAccountNumber = "")
        {
            _personalInfo = new CustomerPersonalInfo("Reza", "Pouya", System.DateTime.Now.AddYears(-32));
            _phoneNumber = new CustomerPhoneNumber("+98 9383810430");
            _customer = new Customer(email, _personalInfo, _phoneNumber, bankAccountNumber);

            return _customer;
        }
    }
}