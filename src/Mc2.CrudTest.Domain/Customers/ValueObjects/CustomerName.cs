using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerName : ValueObject
    {
        public CustomerName(string firstName, string lastName)
        {
            Validate(firstName , isFirstName:true);
            Validate(lastName , isFirstName:false);

            FirstName = firstName;
            LastName = lastName;
        }

        private static void Validate(string inputName, bool isFirstName )
        {
            string name = isFirstName ? "first name" : "last name";

            if (string.IsNullOrEmpty(inputName))
                throw new CustomerException($"The {name} cannot be null or empty");

            if (inputName.Length <= 2)
                throw new CustomerException($"The customer {name} should have more than 2 characters.");

            if (inputName.Length > 64)
                throw new CustomerException($"The customer {name} Length should be less than 65 characters.");
        }


        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}