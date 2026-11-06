using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{ //Detection rule´. Chooses when an alert should issue and actual alert.
    public class AlertConfig : IAuditable
    {
        public Guid Id { get; set; }

        //USed for comparing AlertValue.

        public AlertComparison Comparison { get; set; } = AlertComparison.Equal;

        //The value compared against the alerting source's value to determine if an alert should be triggered.
        public string AlertValue { get; set; }

        //The number of consecutive failures before an alert is triggered.

        public int FailureThreshold { get; set; }

        //Consecutive successes required to resolve an alert. This is used to prevent flapping, where an alert is triggered and resolved repeatedly in a short period of time.
        public int SuccessThreshold { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public Severity Severity { get; set; } = Severity.Warning;

        public AlertSource Source { get; set; } = AlertSource.Internal;

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }



    }
}
