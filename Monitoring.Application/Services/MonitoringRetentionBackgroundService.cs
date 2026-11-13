using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Monitoring.Application.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.Services
{
    public class MonitoringRetentionBackgroundService : BackgroundService
    {
        private readonly ILogger<MonitoringRetentionBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public MonitoringRetentionBackgroundService(
            ILogger<MonitoringRetentionBackgroundService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Monitoring retention service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

                    using var scope = _scopeFactory.CreateScope();

                    var retentionService = scope.ServiceProvider
                        .GetRequiredService<IMonitoringRetentionService>();

                    _logger.LogInformation("Starting monitoring log cleanup.");

                    await retentionService.DeleteExpiredLogsAsync(stoppingToken);

                    _logger.LogInformation("Monitoring log cleanup completed.");
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Monitoring log cleanup failed.");
                }
            }

            _logger.LogInformation("Monitoring retention service stopped.");
        }
    }
}
