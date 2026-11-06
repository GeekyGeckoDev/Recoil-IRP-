using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RecoilDbContext _context;

        public EmployeeRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateEmployeeAsync (Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            return employee.Id;
        }
    }
}
