using Application.AuthApp;
using Application.AuthApp.AuthMessages;
using Application.IServices;
using Application.UserSessionApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shared.DTOs.EmployeeDTOs;
using System.Security.Claims;

namespace APIAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogInService _logInService;
        private readonly IJWTService _jWTService;
        private readonly IUserSessionService _userSessionService;

        private const int MaxFailedLogins = 5;

        private CookieOptions BuildCookieOptions(DateTime expires)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expires
            };
        }


        public AuthController(ILogInService logInService, IJWTService jWTService, IUserSessionService userSessionService)
        {
            _logInService = logInService;
            _jWTService = jWTService;
            _userSessionService = userSessionService;
       
        }

        [HttpPost("login")]
        [EnableRateLimiting("FixedWindow")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
         
            var tokens = await _logInService.ValidateUserAsync(dto);

            Response.Cookies.Append("accessToken", tokens.AccessToken, BuildCookieOptions(DateTime.UtcNow.AddMinutes(15)));
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, BuildCookieOptions(DateTime.UtcNow.AddDays(7)));

            

            return Ok(new
            {
                accessToken = tokens.AccessToken,
                refreshToken = tokens.RefreshToken
            });


        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var tokens = await _logInService.RefreshAsync(refreshToken);

            if (tokens == null)
                return Unauthorized();

            Response.Cookies.Append("accessToken", tokens.AccessToken, BuildCookieOptions(DateTime.UtcNow.AddMinutes(15)));
            Response.Cookies.Append("refreshToken", tokens.RefreshToken, BuildCookieOptions(DateTime.UtcNow.AddDays(7)));

            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var me = await _userSessionService.BuildUserSessionAsync(userId);

            if (me == null)
                return Unauthorized();

            return Ok(me);
        }

    }
}
