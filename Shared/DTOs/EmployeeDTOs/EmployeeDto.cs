using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs.EmployeeDTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }

    public class EmployeeRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
