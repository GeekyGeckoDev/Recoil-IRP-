using Application.IRepos;
using Application.IServices;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos.Responses;
using Shared.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.AuthApp
{
    public class JWTService : IJWTService
    {
        private readonly IUserGetService _userGetService;

        private readonly IConfiguration _configuration;

        private readonly IUserCrudService _userCrudService;

        public JWTService(IUserCrudService employeeCrudService, IConfiguration configuration, IUserGetService employeeGetService) 
        {
            _configuration = configuration;
            _userGetService = employeeGetService;
            _userCrudService = employeeCrudService;
        }

        private string GenerateUserToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:SigningKey"]
                    ?? throw new InvalidOperationException("JWT Signing Key is missing.")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken
                (issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public async Task<TokenResponseDto> CreateUserTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = GenerateUserToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }



        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            await _userCrudService.UpdateUserAsync(user);
            return refreshToken;
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(UserRefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.User_Id, request.RefreshToken);
            if (user == null)
                return null;

            return await CreateUserTokenResponse(user);

        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid user_Id, string refreshToken)
        {
            var user = await _userGetService.GetUserByIdAsync(user_Id);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;
            return user;
        }




    }
}
