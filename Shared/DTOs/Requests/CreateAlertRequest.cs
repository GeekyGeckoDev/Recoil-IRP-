using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs.Requests
{
    public class CreateAlertRequest
    {
        public Guid? AlertConfigId { get; set; }

        public IncidentImpact ImpactAtFireTime { get; set; }

        public string Message { get; set; } = string.Empty;

        public string MessageFingerprint { get; set; } = string.Empty;

        public DateTimeOffset FiredAt { get; set; }

        public AlertSource Source { get; set; }
    }
}
