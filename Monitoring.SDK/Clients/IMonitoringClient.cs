
using Monitoring.Contracts.Events;

namespace Monitoring.SDK.Clients
{
    public interface IMonitoringClient
    {
        bool TrackHttpRequest(HttpRequestMonitoringEvent monitoringEvent);

        bool TrackException(
            Exception exception,
            string? source = null);

        bool TrackLog(
            MonitoringLogLevel level,
            string message,
            string? category = null);
    }
}
