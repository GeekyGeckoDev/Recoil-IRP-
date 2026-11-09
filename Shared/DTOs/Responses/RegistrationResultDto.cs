using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.Responses
{
    public class RegistrationResultDto
    {
        public bool IsAllowed { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
    }
}
