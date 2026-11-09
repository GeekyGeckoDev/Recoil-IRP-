using Application.IRepos;
using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserCrudService : IUserCrudService
    {
        protected readonly IUserCrudRepository _userRepository;

        public UserCrudService (IUserCrudRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateUserAsync (User user)
        {
            return await _userRepository.CreateUserAsync(user);
           
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }
    }
}
