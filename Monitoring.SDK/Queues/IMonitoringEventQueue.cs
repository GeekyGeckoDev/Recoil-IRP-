using Monitoring.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.SDK.Events
{
    public interface IMonitoringEventQueue
    {
        bool TryEnqueue(HttpRequestMonitoringEvent monitoringEvent);

        IAsyncEnumerable<HttpRequestMonitoringEvent> ReadAllAsync(
            CancellationToken cancellationToken);
    }
}
