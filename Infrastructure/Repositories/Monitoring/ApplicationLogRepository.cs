using Infrastructure.Persistence;
using Monitoring.Application.IRepos;
using Monitoring.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Monitoring
{
    public class ApplicationLogRepository : IApplicationLogRepository
    {
        private readonly RecoilDbContext _context;

        public ApplicationLogRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task CreateApplictionLogAsync(ApplicationLog logger)
        {
            await _context.ApplicationLogs.AddAsync(logger);
        }

        public async Task DeleteAllApplicationLogAsync()
        {
            var logs =  _context.ApplicationLogs.ToList();
            _context.ApplicationLogs.RemoveRange(logs);
        }

        public async Task<List<ApplicationLog>> GetAllApplicationLogsAsync(string? searchString, string? logLevel, string? callingMethod, string? callingSource, string? eventId)
        {
            var query = _context.ApplicationLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToLower();

                query = query.Where(l =>
                    l.CallingMethod.ToLower().Contains(searchString) ||
                    l.CallingSource.ToLower().Contains(searchString) ||
                    l.EventId.ToString().Contains(searchString) ||
                    l.LogLevel.ToString().Contains(searchString) ||
                    l.Message.ToString().Contains(searchString));
     
            }

            if(logLevel != null)
            {
                query = query.Where(l => l.LogLevel == logLevel);
            }

            if (callingMethod != null)
            {
                query = query.Where(l => l.CallingMethod == callingMethod);
            }


            if (callingSource != null)
            {
                query = query.Where(l => l.CallingSource == callingSource);
            }

            if (eventId != null)
            {
                query = query.Where(l => l.EventId == eventId);
            }

            return  query
                .OrderByDescending(l => l.Timestamp)
                .ToList();

        }

        public async Task DeleteOlderThanAsync(DateTimeOffset cutoff)
        {
            var logs = _context.ApplicationLogs
                .Where(l => l.Timestamp < cutoff)
                .ToList();

            _context.ApplicationLogs.RemoveRange(logs);
        }
    }
}
