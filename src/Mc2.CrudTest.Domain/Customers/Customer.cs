using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Utility.Helpers;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        /// <summary>
        ///  for ORM mapping
        /// </summary>
        protected Customer()
        { }

        public Customer(string email,
            CustomerName name,
            CustomerDateOfBirth birthDate,
            CustomerPhoneNumber phoneNumber)
        {
            Name = name;
            DateOfBirth = birthDate;
            PhoneNumber = phoneNumber;
            SetEmail(email);
        }

        public string Email { get; protected set; }

        public virtual CustomerName Name { get; protected set; }

        public virtual CustomerDateOfBirth DateOfBirth { get; protected set; }  

        public virtual CustomerPhoneNumber PhoneNumber { get; protected set; }


        private void SetEmail(string email)
        {
            email = email?.Trim() ?? string.Empty;
            
            if (string.IsNullOrEmpty(email))
                throw new CustomerException("The customer email cannot be empty.");

            if(email.Length > 254)
                throw new CustomerException("The email is too long and not valid.");

             if(EmailHelper.IsValidEmail(email) == false)
                throw new CustomerException("The customer email is not valid.");

             this.Email = email;
        }
    }
}