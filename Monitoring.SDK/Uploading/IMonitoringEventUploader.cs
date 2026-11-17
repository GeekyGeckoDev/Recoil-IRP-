
using Monitoring.Contracts.Events;

namespace Monitoring.SDK.Uploading
{
    public interface IMonitoringEventUploader
    {
        Task UploadBatchAsync(
            IReadOnlyCollection<MonitoringEvent> events,
            CancellationToken cancellationToken);
    }
}
