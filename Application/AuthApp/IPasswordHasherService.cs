using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AuthApp
{
    public interface IPasswordHasherService
    {
        string HashPassword(string plainPassword);

        bool VerifyPassword(string plainPassword, string hashedPassword);
    }
}
