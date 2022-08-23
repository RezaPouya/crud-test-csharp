using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using System;

namespace Mc2.CrudTest.Domain.Application.Tests.BasicRecords
{
    internal static class TestCustomers
    {
        internal static Customer customer_1 = new Customer("a@b.com",
           new CustomerPersonalInfo("Mr.A", "Programmer", DateTime.Now.AddYears(-20)),
           new CustomerPhoneNumber("+989383810430"),
           "IR0000001");

        internal static Customer customer_2 = new Customer("a@c.com",
            new CustomerPersonalInfo("Mr.B", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+989383810430"),
            "IR0000001");

        internal static Customer customer_3 = new Customer("a@d.com",
            new CustomerPersonalInfo("Mr.C", "Programmer", DateTime.Now.AddYears(-20)),
            new CustomerPhoneNumber("+989383810430"),
            "IR0000001");
    }
}