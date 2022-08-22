using Mc2.CrudTest.Domain.BaseEntities;
using Mc2.CrudTest.Domain.Helpers;

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
            Number = Convert.ToUInt64(PhoneNumberHelper.SanitizePhoneNumber(number));
        }

        public ulong Number { get; protected set; }
        public void Validate(string number)
        {
            var sanitizedNumber = number.SanitizeToLower();

            if (string.IsNullOrEmpty(sanitizedNumber))
                throw new CustomerException("The customer phone number cannot be empty.");

            if (PhoneNumberHelper.IsValidCellphoneNumber(sanitizedNumber) == false)
                throw new CustomerException("Only cellphone number is acceptable.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }

    /// max phone number characters :  https://stackoverflow.com/questions/723587/whats-the-longest-possible-worldwide-phone-number-i-should-consider-in-sql-varc
    /// Bottom line: 15 chars. If storing prefix and suffix, the bottom line is: 5+15+11=31.
}