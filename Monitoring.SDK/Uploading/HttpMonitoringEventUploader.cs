
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
            IReadOnlyCollection<HttpRequestMonitoringEvent> events,
            CancellationToken cancellationToken)
        {
            if (events.Count == 0)
                return;

            var response = await _httpClient.PostAsJsonAsync(
                Endpoint,
                events,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }
    }
}
