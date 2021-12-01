using OrderSheet.Core.Interfaces;
using System;

namespace OrderSheet.Core
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public DateTime Occurred { get; }

        public BaseDomainEvent()
        {
            Occurred = DateTime.Now;
        }
    }
}
