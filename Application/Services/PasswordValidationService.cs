using Application.IServices;
using Domain.DomainRules;
using Domain.DomainRules.EmployeeRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class PasswordValidationService : IPasswordValidationService
    {
        public async Task<RuleResult> CheckPasswordAsync(string password)
        {
            return PasswordDelegateCheckAll.CheckAll(password,
                PasswordRules.PasswordCannotBeEmpty,
                PasswordRules.PasswordMinLength,
                PasswordRules.PasswordHasUppercase,
                PasswordRules.PasswordHasLowercase,
                PasswordRules.PasswordHasDigit);
        }

    }
}
