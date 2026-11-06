using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class EmployeeService
    {
        protected readonly IEmployeeRepository _employeeRepository;

        public EmployeeService (IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> CreateEmployeeAsync (Employee employee)
        {
            return await _employeeRepository.CreateEmployeeAsync(employee);
        }
    }
}
