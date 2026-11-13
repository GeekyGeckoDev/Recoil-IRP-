using Domain.Enums.Monitoring;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Monitoring
{
    public class CollectorConfiguration : Entity
    {
        public Guid Id { get; private set; }
        public Guid AgentId { get; private set; }

        public CollectorType CollectorType { get; private set; }

        public bool IsEnabled { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }
    }
}
