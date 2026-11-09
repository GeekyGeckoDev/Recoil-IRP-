using Domain.Entities;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Mappers
{
    public static class UserMapper
    {
            public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                Password = null,
                Role = user.Role
            };
        }

        public static User ToUser(UserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                Email = userDto.Email,
                Role = userDto.Role

            };
        }

        public static User ToNewUser(UserRegistrationDto userRegistrationDto)
        {
            return new User
            {
                FirstName = userRegistrationDto.FirstName,
                Email = userRegistrationDto.Email,


            };
        }
    }
}
