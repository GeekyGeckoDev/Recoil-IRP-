using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum TimelineEventType
    {
        Created,
        StatusChanged,
        CommentPosted,
        Aknowledged,
        ServiceAdded,
        ServiceRemoved,
        MergedTo,
        MergedFrom,
        Published,
        Unpublished,
        AlertTriggered,
    }
}
