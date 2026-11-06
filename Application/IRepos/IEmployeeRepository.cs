using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IEmployeeRepository
    {
        Task<Guid> CreateEmployeeAsync(Employee employee);
    }
}
