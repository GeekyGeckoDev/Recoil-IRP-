using System;
using System.Collections.Generic;
using System.Text;


namespace Monitoring.Contracts.Events
{
    public class ExceptionMonitoringEvent : MonitoringEvent
    {
        public string ExceptionType { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? StackTrace { get; set; }

        public string? Source { get; set; }

        public DateTimeOffset OccurredAt { get; set; }
    }
}
