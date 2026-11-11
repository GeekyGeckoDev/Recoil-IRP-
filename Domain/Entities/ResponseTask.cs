using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Domain.Entities
{
    public class ResponseTask
    {
        public Guid Id { get; set; }

        public Guid IncidentId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public Guid AssignedTo { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DueAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }

        public PlaybookPhase? Phase { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}
