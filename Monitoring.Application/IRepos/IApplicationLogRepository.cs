using Monitoring.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.IRepos
{
    public interface IApplicationLogRepository
    {
        Task CreateApplictionLogAsync(ApplicationLog logger);

        Task DeleteAllApplicationLogAsync();

        Task DeleteOlderThanAsync(DateTimeOffset cutoff);

        Task<List<ApplicationLog>> GetAllApplicationLogsAsync(string? searchString, string? logLevel, string? callingMethod, string? callingSource, string? eventId);
    }
}
