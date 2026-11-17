using Microsoft.AspNetCore.Mvc;
using Monitoring.SDK.Clients;
using Monitoring.Contracts.Events;

namespace TestUI.Controllers
{
    [ApiController]
    [Route("test")]
    public class MonitoringTestController : ControllerBase
    {
        private readonly IMonitoringClient _monitoringClient;

        public MonitoringTestController(
            IMonitoringClient monitoringClient)
        {
            _monitoringClient = monitoringClient;
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            return Ok(new
            {
                Message = "Successful test request",
                Timestamp = DateTimeOffset.UtcNow
            });
        }

        [HttpGet("slow")]
        public async Task<IActionResult> Slow()
        {
            await Task.Delay(3000);

            return Ok(new
            {
                Message = "Slow request completed"
            });
        }

        [HttpGet("not-found")]
        public IActionResult NotFoundTest()
        {
            return NotFound(new
            {
                Message = "Simulated resource not found"
            });
        }

        [HttpGet("server-error")]
        public IActionResult ServerError()
        {
            return StatusCode(500, new
            {
                Message = "Simulated server error"
            });
        }

        [HttpGet("exception")]
        public IActionResult ExceptionTest()
        {
            throw new InvalidOperationException(
                "This is a simulated monitoring exception.");
        }

        [HttpGet("log")]
        public IActionResult LogTest()
        {
            _monitoringClient.TrackLog(
                MonitoringLogLevel.Information,
                "This is a manually generated monitoring log.",
                "MonitoringTestController");

            return Ok("Log event queued.");
        }

        [HttpGet("warning")]
        public IActionResult WarningTest()
        {
            _monitoringClient.TrackLog(
                MonitoringLogLevel.Warning,
                "This is a simulated warning.",
                "MonitoringTestController");

            return Ok("Warning event queued.");
        }

        [HttpGet("error-log")]
        public IActionResult ErrorLogTest()
        {
            _monitoringClient.TrackLog(
                MonitoringLogLevel.Error,
                "This is a simulated application error.",
                "MonitoringTestController");

            return Ok("Error log event queued.");
        }
    }
}