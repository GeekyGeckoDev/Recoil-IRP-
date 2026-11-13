using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.SDK.Options
{
    public class MonitoringSdkOptions
    {
        public string BaseAddress { get; set; }
        = "https://localhost:7228";
        public int BatchSize { get; set; } = 100;

        public TimeSpan UploadInterval { get; set; }
            = TimeSpan.FromSeconds(5);

        public int MaxRetryAttempts { get; set; } = 3;

        public TimeSpan InitialRetryDelay { get; set; }
            = TimeSpan.FromSeconds(1);
    }
}