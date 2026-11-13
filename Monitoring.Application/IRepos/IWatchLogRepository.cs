using Monitoring.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.IRepos
{
    public interface IWatchLogRepository
    {
        Task CreateWatchLogAsync(WatchLog watchLog);

        Task<List<WatchLog>> GetPagedWatchLogsAsync(
       string? searchString,
       HttpMethod? httpMethod,
       int? status);

        Task DeleteAllWatchLogsAsync();

        Task DeleteOlderThanAsync(DateTimeOffset cutoff);
    }
}
