using Application.IRepos;
using Monitoring.Application.IRepos;
using Monitoring.Application.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.Services
{
    public class MonitoringRetentionService : IMonitoringRetentionService
    {
        private readonly IWatchLogRepository _httpLogRepository;
        private readonly IExceptionLogRepository _exceptionLogRepository;
        private readonly IApplicationLogRepository _applicationLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MonitoringRetentionService(
            IWatchLogRepository httpLogRepository,
            IExceptionLogRepository exceptionLogRepository,
            IApplicationLogRepository applicationLogRepository,
            IUnitOfWork unitOfWork)
        {
            _httpLogRepository = httpLogRepository;
            _exceptionLogRepository = exceptionLogRepository;
            _applicationLogRepository = applicationLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteExpiredLogsAsync(CancellationToken cancellationToken)
        {
            // Example: delete records older than the configured retention period.
            var cutoff = DateTimeOffset.UtcNow.AddDays(-30);

            await _httpLogRepository.DeleteOlderThanAsync(cutoff);
            await _exceptionLogRepository.DeleteOlderThanAsync(cutoff);
            await _applicationLogRepository.DeleteOlderThanAsync(cutoff);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
