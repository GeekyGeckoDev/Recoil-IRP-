using Application.AuthApp;
using Application.AuthApp.Exceptions;
using Application.IRepos;
using Application.IServices;
using Shared.Dtos.Responses;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{

    public class LogInService : ILogInService
    {
        private readonly IUserGetService _userGetService;

        private readonly IPasswordHasherService _passwordHasherService;

        private readonly IJWTService _jWTService;

        private readonly IUnitOfWork _unitOfWork;

        public LogInService(IUserGetService userGetService, IPasswordHasherService passwordHasherService, IJWTService jWTService, IUnitOfWork unitOfWork)
        {
            _userGetService = userGetService;
            _passwordHasherService = passwordHasherService;
            _jWTService = jWTService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenResponseDto> ValidateUserAsync(LoginDto loginDto)
        {

            var user = await _userGetService.GetUserByEmailAsync(loginDto.Email);

            // Failed logins
            if (user == null)
            {
                throw new LoginException("Invalid credentials");
            }


            return await _jWTService.CreateUserTokenResponse(user);

        }

        public async Task<TokenResponseDto?> RefreshAsync(string refreshToken)
        {
            var user = await _userGetService.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return await _jWTService.CreateUserTokenResponse(user);
        }
    }
}
