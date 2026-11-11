using Application.IRepos;
using Domain.Entities;
using Domain.Enums;
using Domain.Events.Alerts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Events.EventHandlers
{
    public class CreateIncidentFromAlertHandler : IDomainEventHandler<AlertCreatedEvent>
    {

        private readonly IIncidentRepository _incidentRepository;

        public CreateIncidentFromAlertHandler(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }
        public async Task HandleAsync(AlertCreatedEvent domainEvent, CancellationToken cancellation = default)
        {
            var alert = domainEvent.Alert;

            var incident = new Incident
            {
                Id = Guid.NewGuid(),

                Title = $"Alert from {alert.Source}",
                Description = alert.Message,
                Source = alert.Source.ToString(),
                StartDateTime = alert.FiredAt.UtcDateTime,
                Status = IncidentStatus.Investigating
            };

            alert.IncidentId = incident.Id;
            alert.Incident = incident;

            await _incidentRepository.CreateIncidentAsync(incident);
        }

    }
}
