using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers.Events
{
    [Serializable]
    public class CustomerDeletedEto : DomainEvent
    {
        public CustomerDeletedEto()
        {
        }

        public CustomerDeletedEto(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}