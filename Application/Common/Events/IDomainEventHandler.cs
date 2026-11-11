using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Events
{
    //Event handler for domain events.
    public interface IDomainEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        Task HandleAsync(
            TEvent domainEvent,
            CancellationToken cancellationToken = default);

    }
}
