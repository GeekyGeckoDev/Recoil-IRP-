using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainRules.EmployeeRules
{
    public static class PasswordRules
    {
        public static PasswordDelegateCheckAll.PasswordRule PasswordCannotBeEmpty = (password) =>
        string.IsNullOrWhiteSpace(password)
        ? RuleResult.Fail("Password cannot be empty")
        : RuleResult.Success();


        public static PasswordDelegateCheckAll.PasswordRule PasswordMinLength = (password) =>
        password.Length >= 8
        ? RuleResult.Success()
        : RuleResult.Fail("Password must be at least 8 characters long");



        public static PasswordDelegateCheckAll.PasswordRule PasswordHasUppercase = (password) =>
        password.Any(char.IsUpper)
        ? RuleResult.Success()
        : RuleResult.Fail("Password must contain at least one upper case letter");


        public static PasswordDelegateCheckAll.PasswordRule PasswordHasLowercase = (password) =>
        password.Any(char.IsLower)
        ? RuleResult.Success()
        : RuleResult.Fail("Password must contain at least one lowercase letter");

        public static PasswordDelegateCheckAll.PasswordRule PasswordHasDigit = (password) =>
        password.Any(char.IsDigit)
        ? RuleResult.Success()
        : RuleResult.Fail("Password must contain at least one number");
    }
}
