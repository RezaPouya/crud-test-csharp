﻿using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Customers.Mappers;
using Mc2.CrudTest.Domain.Customers.ValueObjects;
using Mc2.CrudTest.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

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
            AddEvent(this.MapToCreateEto());
        }

        [Key]
        public int Id { get; protected set; }
        public string Email { get; protected set; }
        public string BankAccountNumber { get; protected set; }

        public virtual CustomerPersonalInfo PersonalInfo { get; protected set; }

        public virtual CustomerPhoneNumber PhoneNumber { get; protected set; }

        private void SetEmail(string email)
        {
            email = email?.ToLower().Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(email))
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.InvalidEmail);

            if (email.Length > 254)
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.InvalidEmail);

            if (EmailHelper.IsValidEmail(email) == false)
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.InvalidEmail);

            this.Email = email;
        }

        public void Update(string email , string fname, string lname, DateTime dateOfBirth, string bankAcountNumber, string phoneNumber)
        {
            SetBankAccountNumber(bankAcountNumber);
            SetEmail(email);
            PersonalInfo = new CustomerPersonalInfo(fname, lname, dateOfBirth);
            PhoneNumber = new CustomerPhoneNumber(phoneNumber);
            AddEvent(this.MapToUpdateEto());
        }

        private void SetBankAccountNumber(string bankAccountNumber)
        {
            bankAccountNumber = bankAccountNumber?.ToLower()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(bankAccountNumber))
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.InvalidBankAccountNumber); 

            if (bankAccountNumber.Length > 34)
                throw new CustomerException().WithErrorCode(ErrorCodes.CustomerErrorCodes.InvalidBankAccountNumber);

            this.BankAccountNumber = bankAccountNumber;
        }
    }
}