using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WebhookRequestLog
    {
        public Guid Id { get; set; }

        public DateTimeOffset? SourceTimestamp { get; set; }
        public DateTimeOffset ReceivedAt { get; set; }

        public byte[] RequestBody { get; set; } = [];

        public string RequestBodyHash { get; set; } = string.Empty;

        public int? ResponseStatusCode { get; set; }

        public string? ProcessingError { get; set; }
    }
}
