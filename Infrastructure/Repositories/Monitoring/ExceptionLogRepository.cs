using Infrastructure.Persistence;
using Monitoring.Application.IRepos;
using Monitoring.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly RecoilDbContext _context;

        public ExceptionLogRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task CreateExceptionLogAsync(ExceptionLog log)
        {
            await _context.ExceptionLogs.AddAsync(log);
        }

        public async Task DeleteAllExceptionLogsAsync()
        {
            var logs =  _context.ExceptionLogs.ToList();
            _context.ExceptionLogs.RemoveRange(logs);
        }

        public async Task<List<ExceptionLog>> GetPagedExceptionLogsAsync(
        string? searchString,
        string? message,
        string? stackTrace,
        string? source)
        {
            var query = _context.ExceptionLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToLower();

                query = query.Where(l =>
                    l.Message.ToLower().Contains(searchString) ||
                    l.StackTrace.ToLower().Contains(searchString) ||
                    l.Source.ToLower().Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                query = query.Where(l => l.Message.Contains(message));
            }

            if (!string.IsNullOrWhiteSpace(stackTrace))
            {
                query = query.Where(l => l.StackTrace.Contains(stackTrace));
            }

            if (!string.IsNullOrWhiteSpace(source))
            {
                query = query.Where(l => l.Source.Contains(source));
            }

            return  query
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }

        public async Task DeleteOlderThanAsync(DateTimeOffset cutoff)
        {
            var logs = _context.ExceptionLogs
                .Where(l => l.Timestamp < cutoff)
                .ToList();

            _context.ExceptionLogs.RemoveRange(logs);
        }
    }
}
