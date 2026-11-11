using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IAlertRepository
    {
        Task CreateAlertAsync(Alert alert);

        Task UpdateAlertAsync(Alert alert);

        Task<Alert> GetAlertByIdAsync(Guid id);
    }
}
