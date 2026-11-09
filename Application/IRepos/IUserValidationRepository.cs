using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IUserValidationRepository
    {
        Task<bool> EmailExistsAsync(string email);
    }
}
