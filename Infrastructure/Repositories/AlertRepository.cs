using Application.IRepos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly RecoilDbContext _context;


        public AlertRepository(RecoilDbContext context)
        {
            _context = context;
        }

        public async Task CreateAlertAsync(Alert alert)
        {
            await _context.Alerts.AddAsync(alert);


        }

        public async Task UpdateAlertAsync(Alert alert)
        {
            _context.Alerts.Update(alert);

        }

        public async Task<Alert> GetAlertByIdAsync(Guid id)
        {
            return await _context.Alerts.FindAsync(id);

        }
    }
}
