namespace Mc2.CrudTest.Domain.BaseEntities
{
    [Serializable]
    public class DomainEvent
    {
        public DomainEvent(object obj ,  int eventOrder = 0)
        {
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
            EventOrder = eventOrder;
            EventData = obj;
        }

        public Guid EventId { get; protected set; }
        public DateTime EventDateTime { get; protected set; }
        public object EventData { get; protected set; }
        public int EventOrder { get; protected set; }
    }
}
