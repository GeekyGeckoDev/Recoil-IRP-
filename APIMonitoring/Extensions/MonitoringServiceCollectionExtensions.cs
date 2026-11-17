using Microsoft.Extensions.DependencyInjection;
using Monitoring.Application.IServices;
using Monitoring.Application.Services;

namespace Monitoring.API.Extensions
{
    public static class MonitoringServiceCollectionExtensions
    {
        public static IServiceCollection AddMonitoringServices(
            this IServiceCollection services)
        {
            services.AddSignalR();

            services.AddScoped<
                IMonitoringNotifier, SignalRMonitoringNotifier>();

            return services;
        }
    }
}