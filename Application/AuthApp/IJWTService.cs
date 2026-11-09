using Domain.Entities;
using Shared.Dtos.Responses;
using Shared.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AuthApp
{
    public interface IJWTService
    {
        Task<TokenResponseDto> CreateUserTokenResponse(User user);

        Task<TokenResponseDto?> RefreshTokensAsync(UserRefreshTokenRequestDto request);
    }
}
