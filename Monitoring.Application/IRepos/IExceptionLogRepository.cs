using Monitoring.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Application.IRepos
{
    public interface IExceptionLogRepository
    {
        Task CreateExceptionLogAsync(ExceptionLog log);

        Task<List<ExceptionLog>> GetPagedExceptionLogsAsync(
        string? searchString,
        string? message,
        string? stackTrace,
        string? source);

        Task DeleteAllExceptionLogsAsync();

        Task DeleteOlderThanAsync(DateTimeOffset cutoff);
    }
}
