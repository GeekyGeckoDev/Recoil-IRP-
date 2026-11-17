
using Microsoft.AspNetCore.SignalR.Client;
using Monitoring.Contracts.Events;

namespace UI.Clients
{
    public class MonitoringHubClient : IAsyncDisposable
    {
        private readonly HubConnection _connection;

        public event Action<MonitoringEvent>? MonitoringEventReceived;



        public MonitoringHubClient(IConfiguration configuration)
        {
            var hubUrl = configuration["Monitoring:HubUrl"]
                ?? "https://localhost:7228/hubs/monitoring";

            Console.WriteLine($"Monitoring Hub URL: {hubUrl}");

            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            _connection.On<MonitoringEvent>(
                "ReceiveMonitoringEvent",
                monitoringEvent =>
                {
                    MonitoringEventReceived?.Invoke(monitoringEvent);
                });

            _connection.Closed += exception =>
            {
                Console.WriteLine(
                    $"SIGNALR CLOSED: {exception?.ToString() ?? "No exception provided"}");

                return Task.CompletedTask;
            };

            _connection.Reconnecting += exception =>
            {
                Console.WriteLine($"SignalR RECONNECTING: {exception}");
                return Task.CompletedTask;
            };

            _connection.Reconnected += connectionId =>
            {
                Console.WriteLine($"SignalR RECONNECTED: {connectionId}");
                return Task.CompletedTask;
            };
        }



        public async Task StartAsync()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                Console.WriteLine("Starting Monitoring Hub connection...");

                await _connection.StartAsync();

                Console.WriteLine(
                    $"Monitoring Hub connected. State: {_connection.State}");
            }
        }

        public async Task StopAsync()
        {
            if (_connection.State != HubConnectionState.Disconnected)
            {
                await _connection.StopAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("MonitoringHubClient is being disposed!");
            await _connection.DisposeAsync();
        }
    }
}
