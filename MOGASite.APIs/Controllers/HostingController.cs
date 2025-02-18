using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class HostingController(IHostingService hostingService) : ApiBaseController
    {
        private readonly IHostingService _hostingService = hostingService;


        [HttpPost("")]
        public async Task<ActionResult<HostingResponse>> AddHosting([FromForm] AddHostingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newHosting = await _hostingService.AddHostingAsync(request, cancellationToken);
                return Ok(newHosting);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<HostingResponse>>> GetHosting(CancellationToken cancellationToken)
        {
            try
            {
                var hosting = await _hostingService.GetHostingAsync(cancellationToken);
                return Ok(hosting);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HostingResponse>> UpdateHosting(int id, [FromForm] AddHostingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedHosting = await _hostingService.UpdateHostingAsync(id, request, cancellationToken);
                return Ok(updatedHosting);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHosting(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _hostingService.DeleteHostingAsync(id, cancellationToken);
                return Ok(new { Message = $"Hosting with id {id} Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
