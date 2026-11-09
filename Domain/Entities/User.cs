using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? Password_Hash { get; set; }

        public string Email { get; set; } = string.Empty;

        [AllowNull]
        public string? RefreshToken { get; set; }
        [AllowNull]
        public DateTime? RefreshTokenExpiryTime { get; set; }
        [AllowNull]
        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; } = true;

        public Role Role { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }

        public User(Guid id, string firstName, string lastName, string email,Role role)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
 
        }

        public User()
        {

        }
    }

   
}
