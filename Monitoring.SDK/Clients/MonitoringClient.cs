
using Monitoring.Contracts.Events;
using Monitoring.SDK.Queues;

namespace Monitoring.SDK.Clients
{
    public class MonitoringClient : IMonitoringClient
    {
        private readonly IMonitoringEventQueue _eventQueue;

        public MonitoringClient(IMonitoringEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public bool TrackHttpRequest(
            HttpRequestMonitoringEvent monitoringEvent)
        {
            return _eventQueue.TryEnqueue(monitoringEvent);
        }

        public bool TrackException(
            Exception exception,
            string? source = null)
        {
            ArgumentNullException.ThrowIfNull(exception);

            var monitoringEvent = new ExceptionMonitoringEvent
            {
                ExceptionType = exception.GetType().FullName
                    ?? exception.GetType().Name,

                Message = exception.Message,
                StackTrace = exception.StackTrace,
                Source = source ?? exception.Source,
                OccurredAt = DateTimeOffset.UtcNow
            };

            return _eventQueue.TryEnqueue(monitoringEvent);
        }

        public bool TrackLog(
            MonitoringLogLevel level,
            string message,
            string? category = null)
        {
            ArgumentNullException.ThrowIfNull(message);

            var monitoringEvent = new ApplicationLogMonitoringEvent
            {
                Level = level,
                Message = message,
                Category = category,
                OccurredAt = DateTimeOffset.UtcNow
            };

            return _eventQueue.TryEnqueue(monitoringEvent);
        }
    }
}
