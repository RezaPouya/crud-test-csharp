using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers.ValueObjects;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        /// <summary>
        ///  for ORM mapping
        /// </summary>
        protected Customer()
        { }

        public Customer(CustomerName name, 
            CustomerDateOfBirth birthDate,
            CustomerPhoneNumber phoneNumber)
        {
            Name = name;
            DateOfBirth = birthDate;
            PhoneNumber = phoneNumber;
        }

        public virtual CustomerName Name { get; protected set; }

        public virtual CustomerDateOfBirth DateOfBirth { get; protected set; }  

        public virtual CustomerPhoneNumber PhoneNumber { get; protected set; }
    }
}