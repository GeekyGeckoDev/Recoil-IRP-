using Domain.Enums;
using Shared.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IAlertService
    {
        Task CreateAlertAsync(
            CreateAlertRequest request, CancellationToken cancellationToken);
    }
}
