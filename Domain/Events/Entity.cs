using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public abstract class Entity

        //Dispatcher handling of domain events no matter entity used. Registers functionality.
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyCollection<IDomainEvent> DomainEvents
            => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}

