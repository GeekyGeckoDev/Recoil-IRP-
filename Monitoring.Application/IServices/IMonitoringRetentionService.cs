using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.IServices
{
    public interface IMonitoringRetentionService
    {
        Task DeleteExpiredLogsAsync(CancellationToken cancellationToken);
    }
}
