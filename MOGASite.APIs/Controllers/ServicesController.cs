using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class ServicesController(IServiceService serviceService) : ApiBaseController
    {
        private readonly IServiceService _serviceService = serviceService;

        [HttpPost("")]
        public async Task<IActionResult> AddService([FromForm] AddServiceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceService.CreateServiceAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //[Cached(600)]
        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<ServiceResponse>>> GetServices([FromQuery] ServiceByCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceService.GetAllServicesAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetService(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceService.GetServiceByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // [Cached(30)]
        [HttpGet("BySlug/{slug}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceBySlug(string slug, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceService.GetServiceBySlugAsync(slug, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse>> UpdateService(int id, [FromForm] AddServiceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceService.UpdateServiceAsync(id, request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceService.DeleteServiceAsync(id, cancellationToken);
                return Ok(new { Message = "Service Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
