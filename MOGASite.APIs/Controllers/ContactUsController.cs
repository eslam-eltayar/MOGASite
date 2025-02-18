using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class ContactUsController(IContactUsService contactUsService) : ApiBaseController
    {
        private readonly IContactUsService _contactUsService = contactUsService;

        [HttpPost("")]
        public async Task<ActionResult<ContactUsResponse>> AddContactUs([FromBody] AddContactUsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newContactUs = await _contactUsService.AddContactUsAsync(request, cancellationToken);
                return Ok(newContactUs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<ContactUsResponse>>> GetContactUs(CancellationToken cancellationToken)
        {
            try
            {
                var contactUs = await _contactUsService.GetContactUsAsync(cancellationToken);
                return Ok(contactUs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactUs(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _contactUsService.DeleteContactUsAsync(id, cancellationToken);
                return Ok(new { Message = $"ContactUs with id {id} Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
