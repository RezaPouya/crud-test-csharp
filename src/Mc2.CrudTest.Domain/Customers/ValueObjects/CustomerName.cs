using Mc2.CrudTest.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerName : ValueObject
    {
        public string FirstName { get; protected set; } 
        public string LastName { get; protected set; } 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
