using Mc2.CrudTest.Domain.BaseEntities;

namespace Mc2.CrudTest.Domain.Customers.Events
{
    [Serializable]
    public class CustomerDeletedEto : DomainEvent
    {
        public CustomerDeletedEto()
        {
        }

        public CustomerDeletedEto(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}