using Domain.Entities.Monitoring;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Monitoring.Application.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class AgentRegistrationRepository : IAgentRegistrationRepository
    {
        private readonly RecoilDbContext _context;

        public AgentRegistrationRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task<AgentRegistration?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.AgentRegistrations
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<AgentRegistration?> GetByAssetIdAsync(
            Guid assetId,
            CancellationToken cancellationToken = default)
        {
            return await _context.AgentRegistrations
                .FirstOrDefaultAsync(
                    x => x.AssetId == assetId && x.IsActive,
                    cancellationToken);
        }

        public async Task AddAsync(
            AgentRegistration agent,
            CancellationToken cancellationToken = default)
        {
            await _context.AgentRegistrations.AddAsync(
                agent,
                cancellationToken);
        }

        public Task UpdateAsync(
            AgentRegistration agent,
            CancellationToken cancellationToken = default)
        {
            _context.AgentRegistrations.Update(agent);
            return Task.CompletedTask;
        }
    }
}
