using System.Collections.ObjectModel;

namespace Mc2.CrudTest.Domain.BaseEntities
{
    public abstract class AggregateRoot
    {
        protected AggregateRoot()
        { }

        private readonly ICollection<DomainEvent> _distributedEvents = new Collection<DomainEvent>();
        private readonly ICollection<DomainEvent> _localEvents = new Collection<DomainEvent>();

        public virtual IEnumerable<DomainEvent> GetLocalEvents()
        {
            return _localEvents;
        }

        public virtual IEnumerable<DomainEvent> GetDistributedEvents()
        {
            return _distributedEvents;
        }

        public virtual void ClearLocalEvents()
        {
            _localEvents.Clear();
        }

        public virtual void ClearDistributedEvents()
        {
            _distributedEvents.Clear();
        }

        protected virtual void AddLocalEvent(object eventData)
        {
            var lastEvent = _localEvents.OrderBy(p => p.EventOrder).LastOrDefault()?.EventOrder ?? 0;
            _localEvents.Add(new DomainEvent(eventData, lastEvent + 1));
        }

        protected virtual void AddDistributedEvent(object eventData)
        {
            var lastEvent = _distributedEvents.OrderBy(p => p.EventOrder).LastOrDefault()?.EventOrder ?? 0;
            _distributedEvents.Add(new DomainEvent(eventData, lastEvent + 1));
        }
    }
}