using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers.Mappers;
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
            CustomerPersonalInfo personalInfo,
            CustomerPhoneNumber phoneNumber,
            string bankAccountNumber)
        {
            PersonalInfo = personalInfo;
            PhoneNumber = phoneNumber;
            SetEmail(email);
            SetBankAccountNumber(bankAccountNumber);
            AddEvent(this.MapToCreationEto());
        }

        public string Email { get; protected set; }
        public string BankAccountNumber { get; protected set; }

        public virtual CustomerPersonalInfo PersonalInfo { get; protected set; }

        public virtual CustomerPhoneNumber PhoneNumber { get; protected set; }

        private void SetEmail(string email)
        {
            email = email?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(email))
                throw new CustomerException("The customer email cannot be empty.");

            if (email.Length > 254)
                throw new CustomerException("The email is too long and not valid.");

            if (EmailHelper.IsValidEmail(email) == false)
                throw new CustomerException("The customer email is not valid.");

            this.Email = email;
        }

        public void Update(string fname, string lname, DateTime dateOfBirth, string bankAcountNumber, string phoneNumber)
        {
            SetBankAccountNumber(bankAcountNumber);
            PersonalInfo = new CustomerPersonalInfo(fname, lname, dateOfBirth);
            PhoneNumber = new CustomerPhoneNumber(phoneNumber);
        }

        private void SetBankAccountNumber(string bankAccountNumber)
        {
            bankAccountNumber = bankAccountNumber?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(bankAccountNumber))
                throw new CustomerException("The customer bank account number cannot be empty.");

            if (bankAccountNumber.Length > 34)
                throw new CustomerException("The bank account number is too long and not valid.");

            this.BankAccountNumber = bankAccountNumber;
        }
    }
}