using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Monitoring
{
    public class SecurityEvent : Entity
    {

        public Guid Id { get; private set; }
        public Guid AssetId { get; private set; }

        public Guid AgentId { get; private set; }

        public string ExternalEventId { get; private set; }

        public string EventType { get; private set; }

        public string Source { get; private set; }

        public DateTimeOffset OccurredAt { get; private set; }

        public DateTimeOffset ReceivedAt { get; private set; }

        public string? UserId { get; private set; }

        public string? IpAddress { get; private set; }

        public string? Description { get; private set; }

        public Dictionary<string, object> Metadata { get; private set; }
    }
}
