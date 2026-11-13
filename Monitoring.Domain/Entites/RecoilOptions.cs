using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Domain.Entites
{
    public class RecoilOptions
    {
        public string WatchPageUsername { get; set; }
        public string WatchPagePassword { get; set; }
        public string Blacklist { get; set; }
        public string CorsPolicy { get; set; } = string.Empty;
        public bool UseOutputCache { get; set; }
        public bool UseRegexForBlacklisting { get; set; }
    }
}
