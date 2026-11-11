using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly RecoilDbContext _context;

        public IncidentRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task CreateIncidentAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
        }

        public async Task UpdateIncidentAsync(Incident incident)
        {
            _context.Incidents.Update(incident);
        }

        public async Task<Incident> GetIncidentById(Guid id)
        {
            return await _context.Incidents.FindAsync(id);
        }
    }
}
