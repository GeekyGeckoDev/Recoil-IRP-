namespace UI.Clients
{
    public class LiveMonitoringClient
    {
        private readonly HttpClient _httpClient;

        public LiveMonitoringClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
