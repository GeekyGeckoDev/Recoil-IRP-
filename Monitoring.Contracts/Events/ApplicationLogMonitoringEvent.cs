using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Contracts.Events
{
        public class ApplicationLogMonitoringEvent : MonitoringEvent
        {
            public MonitoringLogLevel Level { get; set; }

            public string Message { get; set; } = string.Empty;

            public string? Category { get; set; }

            public DateTimeOffset OccurredAt { get; set; }
        }

}
