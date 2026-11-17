using Microsoft.AspNetCore.SignalR;

namespace APIMonitoring.Hubs
{
    public class MonitoringHub : Hub
    {
        public const string DashboardGroup = "monitoring-dashboard";

        //For managing signal R connections.
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(
     $"Monitoring client connected: {Context.ConnectionId}");

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                DashboardGroup);

            Console.WriteLine(
                $"Client joined group: {DashboardGroup}");

            await base.OnConnectedAsync();
        }
    }
}
