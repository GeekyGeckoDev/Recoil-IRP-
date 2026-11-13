using Application.IRepos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserGetRepository : IUserGetRepository
    {
        private readonly RecoilDbContext _context;

        public UserGetRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByRefreshTokenAsync (string refreshToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        }

        public async Task<User?> GetUserByEmailAsync (string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<User?> GetUserByIdAsync (Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
