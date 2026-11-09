using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IUserCrudRepository 
    {
        Task<Guid> CreateUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(User user);
    }
}
