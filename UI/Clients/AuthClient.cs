using Shared.DTOs.EmployeeDTOs;

namespace UI.Clients
{
    public class AuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginDto dto)
            => await _httpClient.PostAsJsonAsync("api/Auth/login", dto);

        public async Task<HttpResponseMessage> RefreshAsync()
            => await _httpClient.PostAsync("api/Auth/refresh", null);

        public async Task<HttpResponseMessage> LogoutAsync()
            => await _httpClient.PostAsync("api/Auth/logout", null);

        public async Task<HttpResponseMessage> MeAsync()
            => await _httpClient.GetAsync("api/Auth/me");
    }
}
