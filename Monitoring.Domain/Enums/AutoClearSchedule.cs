using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Domain.Enums
{
    public enum AutoClearSchedule
    {
        Daily = 1,
        Weekly,
        Monthly,
        Quarterly,
        Hourly,
        Every6Hours,
        Every12Hours
    }
}
