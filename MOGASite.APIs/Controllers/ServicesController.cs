using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> AddService([FromForm] AddServiceRequest request)
        {
            try
            {
                var result = await _serviceService.CreateServiceAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<ServiceResponse>>> GetServices([FromQuery] ServiceByCategoryRequest request)
        {
            try
            {
                var result = await _serviceService.GetAllServicesAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetService(int id)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse>> UpdateService(int id, [FromForm] AddServiceRequest request)
        {
            try
            {
                var result = await _serviceService.UpdateServiceAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var result = await _serviceService.DeleteServiceAsync(id);
                return Ok(new { Message = "Service Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
