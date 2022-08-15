using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class CustomerName : ValueObject
    {
        public CustomerName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new CustomerException("The first name cannot be null or empty");

            if (firstName.Length <= 2)
                throw new CustomerException($"The customer first name should have more than 2 characters.");

            if (firstName.Length > 64)
                throw new CustomerException($"The customer first name Length should be less than 65 characters.");

            FirstName = firstName;
            LastName = lastName;
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