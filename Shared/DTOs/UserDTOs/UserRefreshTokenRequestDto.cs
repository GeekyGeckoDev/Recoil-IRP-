using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs.UserDTOs
{
    public class UserRefreshTokenRequestDto
    {
        public Guid User_Id { get; set; }
        public required string RefreshToken { get; set; }
    }
}
