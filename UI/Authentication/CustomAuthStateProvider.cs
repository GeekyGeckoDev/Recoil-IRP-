using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace UI.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;
        private readonly AuthManager _authManager;

        public CustomAuthStateProvider(AuthService authService, AuthManager authManager)
        {
            _authService = authService;
            _authService.UserChanged += Notify;
            _authManager = authManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_authService.IsLoggedIn)
            {
                // try restoring from API
                await _authManager.RefreshUserFromApi();
            }

            return new AuthenticationState(_authService.CurrentUser);
        }

        private void Notify(ClaimsPrincipal user)
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(user)));
        }
    }
}
