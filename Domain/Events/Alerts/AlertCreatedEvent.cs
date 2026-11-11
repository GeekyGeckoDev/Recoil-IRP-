using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Alerts
{
    public class AlertCreatedEvent : IDomainEvent
    {
        public Alert Alert { get; }

        public DateTime OccurredOn { get; }

        public AlertCreatedEvent(Alert alert)
        {
            Alert = alert;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
