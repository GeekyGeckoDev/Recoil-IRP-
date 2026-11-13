using Domain.Entities.Monitoring;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.IRepos
{
    public interface IAgentRegistrationRepository
    {
        Task<AgentRegistration?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<AgentRegistration?> GetByAssetIdAsync(
            Guid assetId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            AgentRegistration agent,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            AgentRegistration agent,
            CancellationToken cancellationToken = default);
    }
}
