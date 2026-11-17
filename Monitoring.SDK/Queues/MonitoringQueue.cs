
using System.Threading.Channels;
using Monitoring.Contracts.Events;

namespace Monitoring.SDK.Queues
{
    public class MonitoringEventQueue : IMonitoringEventQueue
    {
        private readonly Channel<MonitoringEvent> _channel;

        public MonitoringEventQueue(int capacity = 1000)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = true,
                SingleWriter = false,
                AllowSynchronousContinuations = false
            };

            _channel = Channel.CreateBounded<MonitoringEvent>(options);
        }

        public bool TryEnqueue(MonitoringEvent monitoringEvent)
        {
            return _channel.Writer.TryWrite(monitoringEvent);
        }

        public IAsyncEnumerable<MonitoringEvent> ReadAllAsync(
            CancellationToken cancellationToken)
        {
            return _channel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
