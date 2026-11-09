using Application.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserValidationRepository : IUserValidationRepository
    {
        private readonly RecoilDbContext _context;

        public UserValidationRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return  _context.Users.Any(u => u.Email == email);
        }
    }
}
