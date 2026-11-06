using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum IncidentEventType
    {
        AlertReceived,
        EvidenceCollected,
        EvidenceAnalyzed,
        IncidentDetected,
        IncidentContained,
        IncidentEradicated,
        IncidentResolved,
        InvestigationNote,
        Other
    }
}
