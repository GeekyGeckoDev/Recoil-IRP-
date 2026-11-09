
using Application.IServices;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserSessionApp
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserGetService _userGetService;

        public UserSessionService(IUserGetService userGetService)
        {
            _userGetService = userGetService;

        }

        public async Task<UserMeDto?> BuildUserSessionAsync(Guid userId)
        {
            var user = await _userGetService.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserMeDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email, 
                Role = user.Role
            };
        }
    }
}
