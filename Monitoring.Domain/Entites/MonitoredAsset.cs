using Domain.Enums.Monitoring;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Monitoring
{
    public class MonitoredAsset : Entity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public AssetType Type { get; private set; }

        public string? Address { get; private set; }

        public bool IsActive { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
    }
}
