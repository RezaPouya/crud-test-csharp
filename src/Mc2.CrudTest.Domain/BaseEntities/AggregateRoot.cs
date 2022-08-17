using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mc2.CrudTest.Domain.BaseEntities
{
    public abstract class AggregateRoot
    {
        protected AggregateRoot()
        { }

        private readonly List<DomainEvent> _events = new();
        
        [NotMapped]
        public IReadOnlyCollection<DomainEvent> DomainEvents => _events.AsReadOnly();

        public virtual List<DomainEvent> GetEvents()
        {
            return _events;
        }

        public virtual void ClearEvents()
        {
            _events.Clear();
        }

        protected virtual void AddEvent(DomainEvent @event)
        {
            var lastEvent = DomainEvents.OrderBy(p => p.EventOrder).LastOrDefault()?.EventOrder ?? 0; 
            @event.SetEventOrder(lastEvent);
            _events.Add(@event);
        }
    }
}