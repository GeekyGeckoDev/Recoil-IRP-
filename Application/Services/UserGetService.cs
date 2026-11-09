using Application.IRepos;
using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserGetService : IUserGetService
    {
        private readonly IUserGetRepository _userGetRepository;

        public UserGetService(IUserGetRepository employeeGetRepository)
        {
            _userGetRepository = employeeGetRepository;
        }

        public async Task<User?> GetUserByIdAsync (Guid id)
        {
            return await _userGetRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByRefreshTokenAsync (string refreshToken)
        {
            return await _userGetRepository.GetUserByRefreshTokenAsync(refreshToken);
        }

        public async Task<User?> GetUserByEmailAsync (string email)
        {
            return await _userGetRepository.GetUserByEmailAsync(email);
        }
    }
}
