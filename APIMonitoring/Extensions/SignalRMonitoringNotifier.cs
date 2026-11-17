using APIMonitoring.Hubs;
using Microsoft.AspNetCore.SignalR;
using Monitoring.Application.IServices;
using Monitoring.Contracts.Events;

namespace Monitoring.Application.Services
{
    public class SignalRMonitoringNotifier : IMonitoringNotifier
    {
        private readonly IHubContext<MonitoringHub> _hubContext;

        public SignalRMonitoringNotifier(IHubContext<MonitoringHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyAsync(
    MonitoringEvent monitoringEvent,
    CancellationToken cancellationToken)
        {
            Console.WriteLine("=== SIGNALR NOTIFICATION START ===");
            Console.WriteLine($"Event ID: {monitoringEvent.EventId}");
            Console.WriteLine($"Event type: {monitoringEvent.GetType().Name}");
            Console.WriteLine($"Target group: {MonitoringHub.DashboardGroup}");

            try
            {
                await _hubContext.Clients
                    .Group(MonitoringHub.DashboardGroup)
                    .SendAsync(
                        "ReceiveMonitoringEvent",
                        monitoringEvent,
                        cancellationToken);

                Console.WriteLine("=== SIGNALR SEND COMPLETED ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== SIGNALR SEND FAILED === {ex}");
                throw;
            }
        }
    }
}
