using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Requests;

namespace APIAlert.Controllers
{
    public class AlertsController(IAlertService alertService) : ControllerBase
    {
        [HttpPost("create-alert")]
        public async Task<IActionResult> CreateAlertAsync([FromBody] CreateAlertRequest request, CancellationToken cancellationToken)
        {
            await alertService.CreateAlertAsync(request, cancellationToken);

            return StatusCode(StatusCodes.Status201Created);

        }

    }
}
