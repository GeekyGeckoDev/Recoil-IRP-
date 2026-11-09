using Domain.DomainRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IPasswordValidationService
    {
        Task<RuleResult> CheckPasswordAsync(string password);
    }
}
