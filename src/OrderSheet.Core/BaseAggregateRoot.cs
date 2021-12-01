using OrderSheet.Core.Interfaces;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace OrderSheet.Core
{
    public abstract class BaseAggregateRoot<TKey> : BaseEntity<TKey>, IAggregateRoot
    {
        private readonly ICollection<IDomainEvent> _events;
        public IReadOnlyCollection<IDomainEvent> Events => _events.ToImmutableArray();

        protected BaseAggregateRoot()
        {
            _events = new Collection<IDomainEvent>();
        }

        protected BaseAggregateRoot(TKey key) 
            : base(key)
        {
            _events = new Collection<IDomainEvent>();
        }

        public void RaiseEvent<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent
        {
            _events.Add(domainEvent);
        }
    }
}
