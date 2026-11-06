using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IncidentImpactChange
    {
        public Guid Id { get; set; }

        public Guid IncidentId { get; set; }

        public Guid ChangedBy { get; set; }
        public IncidentImpact PreviousImpact { get; set; }
        public IncidentImpact NewImpact { get; set; }

        public IncidentImpact Impact { get; set; }

        public DateTimeOffset ChangedAt { get; set; }

        public Incident Incident { get; set; }
    }
}
