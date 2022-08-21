using MediatR;

namespace Mc2.CrudTest.Domain.BaseEntities
{
    [Serializable]
    public abstract class DomainEvent : INotification
    {
        public DomainEvent()
        {
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public Guid EventId { get; protected set; }
        public DateTime EventDateTime { get; protected set; }

        public int EventOrder { get; protected set; }

        public void SetEventOrder(int order)
        {
            EventOrder = order;
        }
    }
}