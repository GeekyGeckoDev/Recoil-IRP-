using Monitoring.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.IServices
{
        public interface IMonitoringNotifier
        {
            Task NotifyAsync(
                MonitoringEvent monitoringEvent,
                CancellationToken cancellationToken);
        }
    
}
