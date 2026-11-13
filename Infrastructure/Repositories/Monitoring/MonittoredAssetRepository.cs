using Domain.Entities.Monitoring;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class MonittoredAssetRepository
    {
        private readonly RecoilDbContext _context;

        public MonittoredAssetRepository (RecoilDbContext context)
        {
            _context = context;
        }

        public async Task CreateMonitoredAssetAsync (MonitoredAsset asset, CancellationToken cancellationToken = default)
        {
            await _context.MonitoredAssets.AddAsync(asset);
        }

        public async Task UpdateMonitoredAssetAsync(MonitoredAsset asset, CancellationToken cancellationToken = default)
        {
            _context.MonitoredAssets.Update(asset);
        }

        public async Task DeleteMonitoredAssetAsync(MonitoredAsset asset, CancellationToken cancellationToken = default)
        {
            _context.MonitoredAssets.Remove(asset);
        }

        public async Task<MonitoredAsset?> GetMonitoredAssetByIdAsync( Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.MonitoredAssets.FindAsync(id);
        }
    }
}
