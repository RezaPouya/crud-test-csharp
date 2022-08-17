using MediatR;

namespace Mc2.CrudTest.Domain.BaseEntities
{
    [Serializable]
    public abstract class DomainEvent : INotification
    {
        public DomainEvent(int eventOrder = 0)
        {
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
            EventOrder = eventOrder;
        }

        public Guid EventId { get; protected set; }
        public DateTime EventDateTime { get; protected set; }
        public int EventOrder { get; protected set; }
    }
}