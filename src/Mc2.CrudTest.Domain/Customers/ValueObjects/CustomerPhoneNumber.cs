﻿using Mc2.CrudTest.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerPhoneNumber : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}