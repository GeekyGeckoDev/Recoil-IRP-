using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.AspNetCore.Models
{
    public class CapturedResponse
    {
        public int StatusCode { get; set; }

        public Dictionary<string, string> Headers { get; set; } = [];

        public string? Body { get; set; }

        public DateTimeOffset FinishTime { get; set; }
    }
}
