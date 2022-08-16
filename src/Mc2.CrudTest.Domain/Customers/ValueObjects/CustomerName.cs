using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerPersonalInfo : ValueObject
    {
        public CustomerPersonalInfo(string firstName, string lastName, DateTime dateOfBirth)
        {
            ValidateName(firstName, isFirstName: true);
            ValidateName(lastName, isFirstName: false);
            ValidateDateOfBirth(dateOfBirth);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            DateOfBirth = dateOfBirth;
        }

        public DateTime DateOfBirth { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        private static void ValidateName(string inputName, bool isFirstName)
        {
            inputName = inputName.Trim();
            string name = isFirstName ? "first name" : "last name";

            if (string.IsNullOrEmpty(inputName))
                throw new CustomerException($"The {name} cannot be null or empty");

            if (inputName.Length <= 2)
                throw new CustomerException($"The customer {name} should have more than 2 characters.");

            if (inputName.Length > 64)
                throw new CustomerException($"The customer {name} Length should be less than 65 characters.");
        }

        private void ValidateDateOfBirth(DateTime birthDate)
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
            yield return FirstName;
            yield return LastName;
            yield return DateOfBirth;   
        }
    }
}