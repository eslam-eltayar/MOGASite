using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class QuotationsController(IQuotationService quotationService) : ApiBaseController
    {
        private readonly IQuotationService _quotationService = quotationService;

        [HttpPost("")]
        public async Task<ActionResult<QuotationResponse>> AddQuotation([FromBody] AddQuotationRequest quotation, CancellationToken cancellationToken)
        {
            try
            {
                var newQuotation = await _quotationService.AddQuotationAsync(quotation, cancellationToken);
                return Ok(newQuotation);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<QuotationResponse>>> GetQuotations(CancellationToken cancellationToken)
        {
            try
            {
                var quotations = await _quotationService.GetQuotationsAsync(cancellationToken);
                return Ok(quotations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotation(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _quotationService.DeleteQuotationAsync(id, cancellationToken);
                return Ok(new { Message = $"Quotation with id {id} Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }



    }
}
