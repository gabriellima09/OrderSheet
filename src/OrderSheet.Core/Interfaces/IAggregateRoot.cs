using System.Collections.Generic;

namespace OrderSheet.Core.Interfaces
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }
        void RaiseEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}
