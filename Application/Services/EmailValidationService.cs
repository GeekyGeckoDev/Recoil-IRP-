using Application.IRepos;
using Application.IServices;
using Domain.DomainRules;
using Domain.DomainRules.EmployeeRules;
using Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class EmailValidationService : IEmailValidationService
    {

        private readonly IUserValidationRepository _userValidationRepository;

        public EmailValidationService(IUserValidationRepository userValidationRepository)
        {
            _userValidationRepository = userValidationRepository;
        }

        public async Task<RuleResult> CheckEmailAsync(UserRegistrationDto userRegistrationDto)
        {
            bool exists = await _userValidationRepository.EmailExistsAsync(userRegistrationDto.Email);

            return EmailDelegateCheckAll.CheckAll(userRegistrationDto.Email,
                EmailRules.EmailCannotBeEmpty,
                EmailRules.EmailHasValidFormat,
                EmailRules.EmailAlreadyExists(_ => exists)
                );
        }


    }
}
