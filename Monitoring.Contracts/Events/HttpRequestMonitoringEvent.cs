using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Contracts.Events
{
    public class HttpRequestMonitoringEvent
    {
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string QueryString { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string? IpAddress { get; set; }

        public int ResponseStatus { get; set; }

        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }

        public Dictionary<string, string> RequestHeaders { get; set; } = [];
        public Dictionary<string, string> ResponseHeaders { get; set; } = [];

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public long DurationMilliseconds { get; set; }
    }
}