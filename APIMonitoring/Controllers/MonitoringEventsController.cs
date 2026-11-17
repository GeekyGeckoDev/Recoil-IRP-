using Microsoft.AspNetCore.Mvc;
using Monitoring.Application.IServices;
using Monitoring.Contracts.Events;

namespace Monitoring.API.Controllers
{
    [ApiController]
    [Route("api/monitoring/events")]
    public class MonitoringEventsController : ControllerBase
    {
        private readonly IMonitoringNotifier _monitoringNotifier;

        public MonitoringEventsController(
            IMonitoringNotifier monitoringNotifier)
        {
            _monitoringNotifier = monitoringNotifier;
        }

        [HttpPost("batch")]
        public async Task<IActionResult> ReceiveBatch(
            [FromBody] List<MonitoringEvent> events,
            CancellationToken cancellationToken)
        {
            foreach (var monitoringEvent in events)
            {
                Console.WriteLine(
    $"Broadcasting event: {monitoringEvent.GetType().Name} - {monitoringEvent.EventId}");
                await _monitoringNotifier.NotifyAsync(monitoringEvent, cancellationToken);
            }

            return Ok(new
            {
                Received = events.Count
            });
        }
    }
}