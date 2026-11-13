using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.AspNetCore.Models
{
    public class CapturedRequest
    {
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string QueryString { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string? IpAddress { get; set; }

        public Dictionary<string, string> Headers { get; set; } = [];

        public string? Body { get; set; }

        public DateTimeOffset StartTime { get; set; }
    }
}
