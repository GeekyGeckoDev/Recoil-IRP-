using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using Monitoring.Application.IRepos;
using Monitoring.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class WatchLogRepository : IWatchLogRepository
    {
        private readonly RecoilDbContext _context;

        public WatchLogRepository (RecoilDbContext context)
        {
            _context = context;
        }

        public async Task CreateWatchLogAsync (WatchLog watchLog)
        {
            await _context.WatchLogs.AddAsync(watchLog);
        }

        public async Task<List<WatchLog>> GetPagedWatchLogsAsync(
       string? searchString,
       HttpMethod? httpMethod,
       int? status)
        {
            var query = _context.WatchLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToLower();

                query = query.Where(l =>
                    l.Path.ToLower().Contains(searchString) ||
                    l.Method.ToLower().Contains(searchString) ||
                    l.ResponseStatus.ToString().Contains(searchString) ||
                    (!string.IsNullOrEmpty(l.QueryString) &&
                     l.QueryString.ToLower().Contains(searchString)));
            }

            if (httpMethod != null)
            {
                query = query.Where(l => l.Method == httpMethod.Method);
            }

            if (status.HasValue)
            {
                query = query.Where(l => l.ResponseStatus == status.Value);
            }

            return await query
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task DeleteAllWatchLogsAsync()
        {
            var logs = await _context.WatchLogs.ToListAsync();
            _context.WatchLogs.RemoveRange(logs);
        }

        public async Task DeleteOlderThanAsync(DateTimeOffset cutoff)
        {
            var logs = _context.WatchLogs
                .Where(l => l.Timestamp < cutoff)
                .ToList();

            _context.WatchLogs.RemoveRange(logs);
        }
    }
}
