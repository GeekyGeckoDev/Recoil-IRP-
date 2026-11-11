using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IIncidentRepository
    {
        Task CreateIncidentAsync(Incident incident);

        Task UpdateIncidentAsync(Incident incident);

        Task<Incident> GetIncidentById(Guid id);
    }
}
