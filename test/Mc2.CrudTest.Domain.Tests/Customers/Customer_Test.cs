﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.Domain.Tests.Customers
{
    public class Customer_Test
    {
        [Fact]
        public void WeShouldHaveCustomer()
        {
            Assert.NotNull(_customer);
        }
    }
}
