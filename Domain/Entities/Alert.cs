using Domain.Enums;
using Domain.Events;
using Domain.Events.Alerts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    //An alert is a signal about a potential  security breach, these are either registered by Recoil or recieved from an external source.

    //Require: register what has been detected, identify where the alert came fromn, register time of detection, identify recurring or double alarms, organizes aknowledgements and assignments, connects to an incident if relevant.
    public class Alert : Entity
    {
        //Adding domainEvents to the alert, så that the dispatcher can read the events directly from the alert, but not add or remove them through this property.
        private readonly List<IDomainEvent> _domainEvents = [];

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
        public Guid Id { get; set; }

        //The process of setting the rules and the thresholds that determine when an alert should be triggered. This could include things like setting a threshold for CPU usage, defining what constitutes an error, etc.
        public Guid? AlertConfigId { get; set; }

        public Guid? IncidentId { get; set; }

        public IncidentImpact ImpactAtFireTime { get; set; }


        //Text/payload at the moment the alert was triggered. This could include things like the error message, the current CPU usage, etc.
        public string Message { get; set; }
        //Should be something Recoil generates and doesn't have to request.

        //A unique identifier for the alert message. This could be a hash of the message, a UUID, or some other unique identifier. The purpose of this is to allow the system to recognize if the same alert has been triggered multiple times, which can help prevent duplicate alerts from being created.
        public string MessageFingerprint { get; set; }

        //External source for the event date time.
        public DateTimeOffset FiredAt { get; set; }

        //When recoil registered the event.
        public DateTimeOffset RegisteredAt { get; private set; }

        public DateTimeOffset? ResolvedAt { get; set; }

        //May need to make a seperate AlertOccurence class to keep information about each seperate occurance with the same fingerprint.

        //How many consecutive failings with the same fingerprint. This is useful for determining if an alert is a one-off event or a recurring issue. For example, if an alert has fired 5 times in a row with the same fingerprint, it might indicate a more serious issue than if it only fired once.
        public int? OccurrenceCount { get; set; } = 1;

        public DateTimeOffset? AcknowledgedAt { get; set; }
        public Guid? AcknowledgedBy { get; set; }

        public IncidentAssignment Assignment { get; set; }

        public AlertSource Source { get; set; } = AlertSource.Internal;

        //The inbound webhook request that created this alert. This is useful for tracking the source of the alert and can help with debugging if there are issues with the alerting system.
        public Guid? SourceRequestLogId { get; set; }

        public string? SourceUrl { get; set; }

        public AlertConfig? AlertConfig { get; set; }

        public Incident? Incident { get; set; }

        public WebhookRequestLog? SourceRequestLog { get; set; }

            public Alert(
        Guid? alertConfigId,
        IncidentImpact impactAtFireTime,
        string message,
        string messageFingerprint,
        DateTimeOffset firedAt,
        AlertSource source)
            {
                Id = Guid.NewGuid();
                AlertConfigId = alertConfigId;
                ImpactAtFireTime = impactAtFireTime;
                Message = message;
                MessageFingerprint = messageFingerprint;
                FiredAt = firedAt;
                RegisteredAt = DateTimeOffset.UtcNow;
                Source = source;
                OccurrenceCount = 1;

                AddDomainEvent(new AlertCreatedEvent(this));
            }

        public Alert() { }
    }
}
