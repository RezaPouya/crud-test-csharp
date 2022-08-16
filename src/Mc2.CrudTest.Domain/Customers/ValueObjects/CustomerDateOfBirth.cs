using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerDateOfBirth : ValueObject
    {
        protected CustomerDateOfBirth()
        {
        }

        public CustomerDateOfBirth(DateTime date)
        {
            Validate(date);
            Date = date;
        }

        public DateTime Date { get; protected set; }

        private void Validate(DateTime birthDate)
        {
            if (birthDate == default(DateTime))
                throw new CustomerException("The date of birth is not valid.");

            if (birthDate.Date > DateTime.Now.Date)
                throw new CustomerException("The date of birth cannot set to future.");

            if (birthDate.Date < DateTime.Now.AddYears(-125))
                throw new CustomerException("The date of birth is not valid.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
        }
    }
}