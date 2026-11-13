using Domain.Events;
using Monitoring.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Monitoring
{
    public class AgentRegistration : Entity
    {
        public Guid Id { get; private set; }
        public Guid AssetId { get; private set; }

        public string Name { get; private set; }

        public string AgentVersion { get; private set; }

        public OperatingSystemType OperatingSystem { get; private set; }

        public DateTimeOffset RegisteredAt { get; private set; }

        public DateTimeOffset? LastHeartbeat { get; private set; }

        public bool IsActive { get; private set; }
    }
}
