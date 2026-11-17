
using Monitoring.Domain.Entities.Monitoring;

namespace Monitoring.Application.Interfaces.Repositories
{
    public interface IMonitoringEventRepository
    {
        Task AddRangeAsync(
            IEnumerable<MonitoringEventRecord> events,
            CancellationToken cancellationToken);
    }
}
