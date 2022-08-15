using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers
{
    public  class Customer : AggregateRoot
    {
        public CustomerName Name { get; protected set; }
    }
}
