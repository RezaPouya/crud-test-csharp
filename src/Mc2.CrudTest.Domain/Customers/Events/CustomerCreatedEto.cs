using Mc2.CrudTest.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.Events
{
    [Serializable]
    public class CustomerCreatedEto : DomainEvent
    {
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
