using Mc2.CrudTest.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerPhoneNumber : ValueObject
    {
        protected CustomerPhoneNumber()
        {
        }
        public CustomerPhoneNumber(string number)
        {
            Validate(number);
            Number = number.Trim();
        }


        public string Number { get; set; }

        public void  Validate(string number)
        {
            var trimedNumber = number.Trim();
            if (string.IsNullOrEmpty(trimedNumber))
                throw new CustomerException("The customer phone number cannot be empty.");

            if (trimedNumber.Length > 31)
                throw new CustomerException("The customer phone number is not valid.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }

    /// max phone number characters :  https://stackoverflow.com/questions/723587/whats-the-longest-possible-worldwide-phone-number-i-should-consider-in-sql-varc
    /// Bottom line: 15 chars. If storing prefix and suffix, the bottom line is: 5+15+11=31.
}
