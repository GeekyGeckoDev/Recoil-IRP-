using Domain.DomainRules;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IEmailValidationService
    {
        Task<RuleResult> CheckEmailAsync(UserRegistrationDto registrationDto);
    }
}
