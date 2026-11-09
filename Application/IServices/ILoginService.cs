using Shared.Dtos.Responses;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface ILogInService
    {
        Task<TokenResponseDto> ValidateUserAsync(LoginDto loginDto);

        Task<TokenResponseDto?> RefreshAsync(string refreshToken);
    }
}
