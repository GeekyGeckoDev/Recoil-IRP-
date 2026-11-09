using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.Responses
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
