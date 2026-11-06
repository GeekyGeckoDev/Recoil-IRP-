using Domain.Enums;
using Shared.DTOs.EmployeeDTOs;
using System.Security.Claims;

namespace UI.Authentication
{
    public class AuthService
    {
        public event Action<ClaimsPrincipal>? UserChanged;

        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public ClaimsPrincipal CurrentUser => _currentUser;

        public bool IsLoggedIn => _currentUser.Identity?.IsAuthenticated ?? false;

        public Guid Id { get; private set; }

        public string? FirstName { get; private set; }

        public Role Role { get; private set; }

        public string? Email { get; private set; }

        public void SetUser(UserMeDto user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.FirstName ?? ""),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email ?? ""),
                AssignEmployeeRole(user.Role)
            };

            var identity = new ClaimsIdentity(
                claims,
                "RecoilAuth"
            );

            _currentUser = new ClaimsPrincipal(identity);

            Id = user.Id;
            FirstName = user.FirstName;
            Role = user.Role;
            Email = user.Email;

            UserChanged?.Invoke(_currentUser);
        }

        public void Logout()
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

            Id = Guid.Empty;
            FirstName = null;
            Email = null;
            Role = Role.none;
        }

        private Claim AssignEmployeeRole(Role role)
        {

            return role switch
            {
                Role.Administrator => new Claim(ClaimTypes.Role, "Administrator"),
                Role.SecurityAnalyst => new Claim(ClaimTypes.Role, "SecurityAnalyst"),
                Role.IncidentResponder => new Claim(ClaimTypes.Role, "IncidentResponder"),
                Role.ReadOnly => new Claim(ClaimTypes.Role, "ReadOnly"),

                _ => throw new ArgumentOutOfRangeException(nameof(role))
            };
        }
    }
}