
using System.Net.Http.Json;
using Monitoring.Contracts.Events;

namespace Monitoring.SDK.Uploading
{
    public class HttpMonitoringEventUploader : IMonitoringEventUploader
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint =
            "/api/monitoring/events/batch";

        public HttpMonitoringEventUploader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task UploadBatchAsync(
            IReadOnlyCollection<MonitoringEvent> events,
            CancellationToken cancellationToken)
        {
            if (events.Count == 0)
                return;

            var requestUri = new Uri(_httpClient.BaseAddress!, Endpoint);

            Console.WriteLine("=== MONITORING SDK UPLOAD ===");
            Console.WriteLine($"Destination: {requestUri}");
            Console.WriteLine($"Event count: {events.Count}");

            foreach (var monitoringEvent in events)
            {
                Console.WriteLine(
                    $"Uploading: {monitoringEvent.GetType().Name} " +
                    $"| ID: {monitoringEvent.EventId}");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    Endpoint,
                    events,
                    cancellationToken);

                Console.WriteLine(
                    $"Upload response: {(int)response.StatusCode} " +
                    $"{response.StatusCode}");

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UPLOAD FAILED: {ex}");
                throw;
            }
        }

    }
}
