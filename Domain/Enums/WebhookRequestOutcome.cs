using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum WebhookRequestOutcome
    {
        Accepted,
        // The webhook request was accepted, but the incident was not found. This could happen if the incident was deleted or if the webhook was sent for an incident that does not exist.
        AcceptedOrphan,

        //Produced or updated an orphan Alert because the sources correlation id did not match any existing incident. This is a special case of AcceptedOrphan, but is useful to distinguish in the logs.
        CorrelationMismatch,

        AuthFailed,
        ParseError,

        //A resend of an already processed webhook request. This is not an error, but it is useful to distinguish in the logs.
        Duplicate,

    }
}
