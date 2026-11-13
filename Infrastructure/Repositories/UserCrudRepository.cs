using Application.IRepos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserCrudRepository : IUserCrudRepository
    {
        private readonly RecoilDbContext _context;

        public UserCrudRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateUserAsync (User user)
        {
            await _context.Users.AddAsync(user);
            return user.Id;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);

        }
    }
}
