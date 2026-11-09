using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs.EmployeeDTOs
{
    public class RegistrationResultDto
    {
        public bool IsAllowed { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
    }
}
