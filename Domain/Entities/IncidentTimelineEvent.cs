using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IncidentTimelineEvent
    {
        public Guid Id { get; set; }

        public Guid IncidentId { get; set; }

        public IncidentEventType EventType { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTimeOffset OccurredAt { get; set; }
        public DateTimeOffset RecordedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public Incident Incident { get; set; } = null!;
    }
}
