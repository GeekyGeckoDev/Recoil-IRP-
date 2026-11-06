using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum AlertSource
    {
        Internal,

        //External sources are not currently supported, but this is here for future expansion.
        //Could be used to recieve via the GPC Cloud Monitoring API, or other external alerting systems.
        GcpCloudMonitoring
    }
}
