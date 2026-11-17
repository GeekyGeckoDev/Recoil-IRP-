namespace Monitoring.Domain.Entities.Monitoring
{
    public class MonitoringEventRecord
    {
        public Guid Id { get; set; }

        public Guid ExternalEventId { get; set; }

        public string EventType { get; set; } = string.Empty;

        public DateTimeOffset ReceivedAt { get; set; }

        public string Payload { get; set; } = string.Empty;
    }
}