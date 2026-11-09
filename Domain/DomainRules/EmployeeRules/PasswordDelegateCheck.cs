using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainRules.EmployeeRules
{
    public class PasswordDelegateCheckAll
    {
        public delegate RuleResult PasswordRule(string password);

        public static RuleResult CheckAll(string password, params PasswordRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(password);
                if (!result.IsAllowed)
                    return result;

            }

            return RuleResult.Success();
        }
    }
}
