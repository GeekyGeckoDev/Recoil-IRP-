using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IUserCrudService
    {
        Task<Guid> CreateUserAsync(User employee);

        Task DeleteUserAsync(User user);

        Task UpdateUserAsync(User user);
    }
}
