using Domain.DomainRules;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IUserManagerService
    {
        Task<RuleResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
    }
}
