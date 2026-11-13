using Monitoring.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.SDK.Uploading
{
    public interface IMonitoringEventUploader
    {
        Task UploadBatchAsync(
            IReadOnlyCollection<HttpRequestMonitoringEvent> events,
            CancellationToken cancellationToken);
    }
}
