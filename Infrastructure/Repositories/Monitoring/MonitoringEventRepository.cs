using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Monitoring.Application.Interfaces.Repositories;
using Monitoring.Domain.Entities.Monitoring;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recoil.Infrastructure.Repositories.Monitoring
{
    public class MonitoringEventRepository : IMonitoringEventRepository
    {
        private readonly RecoilDbContext _context;

        public MonitoringEventRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(
            IEnumerable<MonitoringEventRecord> events,
            CancellationToken cancellationToken)
        {
            await _context.MonitoringEvents.AddRangeAsync(
                events,
                cancellationToken);
        }
    }
}
