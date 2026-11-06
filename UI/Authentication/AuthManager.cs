using Shared.DTOs.EmployeeDTOs;
using System.Text.Json;
using UI.Clients;
using UI.Exstensions;

namespace UI.Authentication
{
    public class AuthManager
    {
        private readonly AuthClient _authClient;

        private readonly AuthService _authService;

        private readonly ITokenStore _tokenStore;

        public AuthManager(AuthClient authClient, AuthService authService, ITokenStore tokenStore)
        {
            _authClient = authClient;
            _authService = authService;
            _tokenStore = tokenStore;
        }

        public async Task<bool> LoginAsync (LoginDto dto)
        {
            var response = await _authClient.LoginAsync(dto);

            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginResponseDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _tokenStore.SetAsync(loginResult.AccessToken);

            return true;
        }

        public async Task<bool> RefreshUserFromApi()
        {
            var meResponse = await _authClient.MeAsync();

            if (!meResponse.IsSuccessStatusCode)
            {
                _authService.Logout();
                return false;
            }

            var json = await meResponse.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<UserMeDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (user == null)
            {
                _authService.Logout();
                return false;
            }

            _authService.SetUser(user);
            return true;

        }

        public async Task LogOutAsync ()
        {
            await _authClient.LogoutAsync();
            _authService.Logout();
        }
    }
}
