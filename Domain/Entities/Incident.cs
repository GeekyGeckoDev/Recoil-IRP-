using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Incident : Entity, IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }

        public Playbook Playbook { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        //Current status of the incident. This is a simple enum, not a separate entity, because the set of statuses is fixed and small.
        public IncidentStatus Status { get; set; } = IncidentStatus.Investigating;

        public string Source { get; set; }

        public DateTime ModifiedAt { get; set; }

        public ICollection<IncidentTimelineEvent> TimelineEvents { get; set; }

        public ICollection<IncidentImpactChange> ImpactChanges { get; set; }

        public ICollection<IncidentAssignment> Assignments { get; set; } = []; Id

        public DateTime? DetectedAt { get; set; }
        public DateTime? ContainedAt { get; set; }
        public DateTime? EradicatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public List<string> AffectedAssets { get; set; } = [];

        public ICollection<EvidenceCollection> EvidenceCollections;

        public List<ResponseTask> Tasks { get; set; } = [];


        public Dictionary<string, string> CustomFields { get; set; } = new();

        public static Incident Create(
       string title,
       string description,
       string source)
        {
            var incident = new Incident
            {
                Title = title,
                Description = description,
                Source = source,
                StartDateTime = DateTime.UtcNow,
                Status = IncidentStatus.Investigating
            };

            incident.AddDomainEvent(
                new IncidentCreatedEvent(
                    incident.Id,
                    incident.Title,
                    DateTimeOffset.UtcNow
                )
            );

            return incident;
        }

    }
}
