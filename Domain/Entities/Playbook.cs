using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Playbook
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public IncidentCategory Category { get; set; }

        public Severity MinimumSeverity { get; set; } = Severity.Low;

        public ICollection<PlaybookStep> Steps { get; set; } = new List<PlaybookStep>();

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}
