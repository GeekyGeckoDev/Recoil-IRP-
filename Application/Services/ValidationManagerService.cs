using Application.IServices;
using Domain.DomainRules;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ValidationManagerService : IValidationManagerService
    {
        private readonly IPasswordValidationService _passwordValidationService;
    
        public ValidationManagerService(IPasswordValidationService passwordValidationService)
        {
            _passwordValidationService = passwordValidationService;
        }

        public async Task<RuleResult> FinalValidationAsync (UserRegistrationDto registrationDto)
        {
            var passwordCheck = await _passwordValidationService.CheckPasswordAsync(registrationDto.Password);
            if (!passwordCheck.IsAllowed)
                new RegistrationResultDto
                {
                    IsAllowed = false,
                    Message = passwordCheck.Message
                };

            return RuleResult.Success();
        }
    }
}
