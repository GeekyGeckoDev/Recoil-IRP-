using Monitoring.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;


namespace Monitoring.SDK.Queues
{
    public interface IMonitoringEventQueue
    {
        bool TryEnqueue(MonitoringEvent monitoringEvent);

        IAsyncEnumerable<MonitoringEvent> ReadAllAsync(
            CancellationToken cancellationToken);
    }
}
