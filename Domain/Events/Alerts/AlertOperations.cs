using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Alerts
{
    public class AlertOperations
    {
        public static Alert Create(Alert alert)
        {
            return new Alert(
                alert.AlertConfigId,
                alert.ImpactAtFireTime,
                alert.Message,
                alert.MessageFingerprint,
                alert.FiredAt,
                alert.Source);
        }
    }
}

