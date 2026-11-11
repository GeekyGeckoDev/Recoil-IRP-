using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Events
{
    //Makes sure it dispatches the events to the right handlers.
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default);

    }
}
