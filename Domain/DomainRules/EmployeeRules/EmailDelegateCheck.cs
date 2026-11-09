using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainRules.EmployeeRules
{
    public class EmailDelegateCheckAll
    {
        public delegate RuleResult EmailRule(string email);

        public static RuleResult CheckAll(string email, params EmailRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(email);
                if (!result.IsAllowed)
                    return result;
            }
            return RuleResult.Success();
        }
    }
}
