using Application.Common.Events;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Events
{
    public class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
    {
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await DispatchEventAsync((dynamic)domainEvent, cancellationToken);
            }
        }

        private async Task DispatchEventAsync<TEvent>(
          TEvent domainEvent,
          CancellationToken cancellationToken)
          where TEvent : IDomainEvent
        {
            //Uses dependency injection to locate the correct handler based on the handle TEvent.
            var handlers =
                serviceProvider.GetServices<IDomainEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(
                    domainEvent,
                    cancellationToken);
            }
        }
    }
}
