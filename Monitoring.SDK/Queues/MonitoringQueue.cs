using Monitoring.Contracts.Events;
using Monitoring.SDK.Events;
using System.Threading.Channels;

namespace Monitoring.SDK.Queues
{
    public class MonitoringEventQueue : IMonitoringEventQueue
    {
        private readonly Channel<HttpRequestMonitoringEvent> _channel;

        public MonitoringEventQueue(int capacity = 1000)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = true,
                SingleWriter = false,
                AllowSynchronousContinuations = false
            };

            _channel = Channel.CreateBounded<HttpRequestMonitoringEvent>(
                options);
        }

        public bool TryEnqueue(HttpRequestMonitoringEvent monitoringEvent)
        {
            return _channel.Writer.TryWrite(monitoringEvent);
        }

        public IAsyncEnumerable<HttpRequestMonitoringEvent> ReadAllAsync(
            CancellationToken cancellationToken)
        {
            return _channel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}