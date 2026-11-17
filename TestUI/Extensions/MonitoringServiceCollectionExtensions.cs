using Monitoring.SDK.Extensions;
using Monitoring.SDK.Options;

namespace TestUI.Extensions
{
    public static class MonitoringServiceCollectionExtensions
    {
        public static IServiceCollection AddMonitoringServices(
            this IServiceCollection services,
            Action<MonitoringSdkOptions>? configureOptions = null)
        {
            services.AddMonitoringSdk(configureOptions);

            return services;
        }
    }
}