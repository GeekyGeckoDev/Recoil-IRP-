using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IUserGetService
    {
        Task<User?> GetUserByIdAsync(Guid id);

        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);

        Task<User?> GetUserByEmailAsync(string email);
    }
}
