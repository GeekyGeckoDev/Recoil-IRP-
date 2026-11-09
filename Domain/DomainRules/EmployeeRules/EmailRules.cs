using Domain.Validator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainRules.EmployeeRules
{
    public static class EmailRules
    {
        public static EmailDelegateCheckAll.EmailRule EmailCannotBeEmpty = (email) =>
            string.IsNullOrWhiteSpace(email)
                ? RuleResult.Fail("Email cannot be empty")
                : RuleResult.Success();

        public static EmailDelegateCheckAll.EmailRule EmailHasValidFormat = (email) =>
            EmailValidator.IsValidEmail(email)
                ? RuleResult.Success()
                : RuleResult.Fail("Invalid email format");

        public static EmailDelegateCheckAll.EmailRule EmailAlreadyExists(Func<string, bool> existsFunc) =>
            (email) => existsFunc(email)
                ? RuleResult.Fail("This email is already registered")
                : RuleResult.Success();
    }
}
