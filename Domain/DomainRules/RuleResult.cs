using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainRules
{
    public class RuleResult
    {
        public bool IsAllowed { get; set; }
        public string Message { get; set; }

        public RuleResult(bool isAllowed, string message = "")
        {
            IsAllowed = isAllowed;
            Message = message;
        }

        public static RuleResult Success() => new RuleResult(true);
        public static RuleResult Fail(string message) => new RuleResult(false, message);
    }
}
