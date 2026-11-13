using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums.Monitoring
{
    public enum EventProcessingStatus
    {
        Pending,
        Processing,
        Processed,
        Failed
    }
}
