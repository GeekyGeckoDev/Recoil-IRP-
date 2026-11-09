using Application.AuthApp;
using Application.IRepos;
using Application.IServices;
using Domain.DomainRules;
using Shared.DTOs.EmployeeDTOs;
using Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserCrudService _userCrudService;

        private readonly IPasswordHasherService _passwordHasherService;

        private readonly IValidationManagerService _validationManagerService;

        private readonly IUnitOfWork _unitOfWork;



        public UserManagerService(IUserCrudService userCrudService, IPasswordHasherService passwordHasherService,
            IValidationManagerService validationManagerService, IUnitOfWork unitOfWork)
        {
            _userCrudService = userCrudService;
            _passwordHasherService = passwordHasherService;
            _validationManagerService = validationManagerService;
            _unitOfWork = unitOfWork;


        }

        public async Task<RuleResult> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            var validationCheck = await _validationManagerService.FinalValidationAsync(userRegistrationDto);

            if (!validationCheck.IsAllowed)
                return validationCheck;


            try
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var user = UserMapper.ToNewUser(userRegistrationDto);

                    user.Password_Hash = _passwordHasherService.HashPassword(userRegistrationDto.Password);



                    await _userCrudService.CreateUserAsync(user);
                });

                return RuleResult.Success();
            }

            catch (Exception ex)
            {
                return RuleResult.Fail($"Estate creation failed: {ex.Message}");
            }
        }
    }
}
