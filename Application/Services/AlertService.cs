using Application.IRepos;
using Application.IServices;
using Domain.Enums;
using Domain.Events.Alerts;
using Shared.DTOs.Requests;
using Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class AlertService(
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork) : IAlertService
    {
        public async Task CreateAlertAsync(
            CreateAlertRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.ExecuteAsync(async () =>
            {
                var alert = AlertOperations.Create(AlertMapper.ToAlert(request));

                await alertRepository.CreateAlertAsync(alert);
            }, cancellationToken);
        }
    }
}
