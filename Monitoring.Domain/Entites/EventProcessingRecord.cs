using Domain.Enums.Monitoring;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Monitoring
{
    public class EventProcessingRecord : Entity
    {
        public Guid Id { get; private set; }
        public Guid SecurityEventId { get; private set; }

        public EventProcessingStatus Status { get; private set; }

        public int RetryCount { get; private set; }

        public DateTimeOffset? ProcessedAt { get; private set; }

        public string? ErrorMessage { get; private set; }
    }
}
