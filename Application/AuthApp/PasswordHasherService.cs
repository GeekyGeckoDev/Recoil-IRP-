using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AuthApp
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(plainPassword);
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(plainPassword, hashedPassword);
        }
    }
}
