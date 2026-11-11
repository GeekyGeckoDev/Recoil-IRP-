using Domain.Entities;
using Shared.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Mappers
{
    public static class AlertMapper
    {
        public static Alert ToAlert (CreateAlertRequest request)
        {
            return new Alert
            {
                AlertConfigId = request.AlertConfigId,
                ImpactAtFireTime = request.ImpactAtFireTime,
                Message = request.Message,
                MessageFingerprint = request.MessageFingerprint,
                FiredAt = request.FiredAt,
                Source = request.Source,
            };
        }
    }
}
