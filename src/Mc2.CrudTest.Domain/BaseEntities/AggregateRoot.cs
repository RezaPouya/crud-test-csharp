using System.Collections.ObjectModel;

namespace Mc2.CrudTest.Domain.BaseEntities
{
    public abstract class AggregateRoot
    {
        protected AggregateRoot()
        { }

        private readonly ICollection<DomainEvent> _events = new Collection<DomainEvent>();

        public virtual IEnumerable<DomainEvent> GetEvents()
        {
            return _events;
        }

        public virtual void ClearEvents()
        {
            _events.Clear();
        }

        protected virtual void AddEvent(object eventData)
        {
            var lastEvent = _events.OrderBy(p => p.EventOrder).LastOrDefault()?.EventOrder ?? 0;
            _events.Add(new DomainEvent(eventData, lastEvent + 1));
        }
    }
}