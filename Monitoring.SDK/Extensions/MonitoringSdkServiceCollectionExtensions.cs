
using Microsoft.Extensions.DependencyInjection;
using Monitoring.SDK.Background;
using Monitoring.SDK.Background.Monitoring.SDK.Background;
using Monitoring.SDK.Events;
using Monitoring.SDK.Options;
using Monitoring.SDK.Queues;
using Monitoring.SDK.Uploading;

namespace Monitoring.SDK.Extensions
{
    public static class MonitoringSdkServiceCollectionExtensions
    {
        public static IServiceCollection AddMonitoringSdk(
            this IServiceCollection services,
            Action<MonitoringSdkOptions>? configureOptions = null)
        {
            services.AddOptions<MonitoringSdkOptions>();

            if (configureOptions is not null)
            {
                services.Configure(configureOptions);
            }

            services.AddSingleton<
                IMonitoringEventQueue,
                MonitoringEventQueue>();

            services.AddHttpClient<
                IMonitoringEventUploader,
                HttpMonitoringEventUploader>((serviceProvider, client) =>
                {
                    var options = serviceProvider
                        .GetRequiredService<
                            Microsoft.Extensions.Options.IOptions<MonitoringSdkOptions>>()
                        .Value;

                    client.BaseAddress = new Uri(options.BaseAddress);
                });

            services.AddHostedService<MonitoringEventProcessor>();

            return services;
        }
    }
}
