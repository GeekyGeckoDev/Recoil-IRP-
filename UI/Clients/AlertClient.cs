using Microsoft.AspNetCore.Http.HttpResults;
using Shared.DTOs.Requests;

namespace UI.Clients
{
    public class AlertClient
    {
        private readonly HttpClient _httpClient;

        public AlertClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateAlertAsync (CreateAlertRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Alerts/create-alert", request);

            return response;
        }
    }
}
