using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IUserGetRepository
    {
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByIdAsync(Guid id);


    }
}
