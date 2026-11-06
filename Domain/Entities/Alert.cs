using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    //An alert is a signal about a potential  security breach, these are either registered by Recoil or recieved from an external source.

    //Require: register what has been detected, identify where the alert came fromn, register time of detection, identify recurring or double alarms, organizes aknowledgements and assignments, connects to an incident if relevant.
    public class Alert
    {
        public Guid Id { get; set; }

        //The process of setting the rules and the thresholds that determine when an alert should be triggered. This could include things like setting a threshold for CPU usage, defining what constitutes an error, etc.
        public Guid? AlertConfigId { get; set; }

        public Guid? IncidentId { get; set; }

        public IncidentImpact ImpactAtFireTime { get; set; }


        //Text/payload at the moment the alert was triggered. This could include things like the error message, the current CPU usage, etc.
        public string Message { get; set; }

        //A unique identifier for the alert message. This could be a hash of the message, a UUID, or some other unique identifier. The purpose of this is to allow the system to recognize if the same alert has been triggered multiple times, which can help prevent duplicate alerts from being created.
        public string MessageFingerprint { get; set; }

        public DateTimeOffset FiredAt { get; set; }

        public DateTimeOffset? ResolvedAt { get; set; }

        //May need to make a seperate AlertOccurence class to keep information about each seperate occurance with the same fingerprint.

        //How many consecutive failings with the same fingerprint. This is useful for determining if an alert is a one-off event or a recurring issue. For example, if an alert has fired 5 times in a row with the same fingerprint, it might indicate a more serious issue than if it only fired once.
        public int? OccurrenceCount { get; set; } = 1;

        public DateTimeOffset? AcknowledgedAt { get; set; }
        public Guid? AcknowledgedBy { get; set; }

        public IncidentAssignment Assignment { get; set; }

        public AlertSource Source { get; set; } = AlertSource.Internal;

        //The inbound webhook request that created this alert. This is useful for tracking the source of the alert and can help with debugging if there are issues with the alerting system.
        public Guid? SourceRequstLogId { get; set; }

        public string? SourceUrl { get; set; }

        public AlertConfig? AlertConfig { get; set; }

        public Incident? Incident { get; set; }

        public WebhookRequestLog? SourceRequestLog { get; set; }
    }
}
