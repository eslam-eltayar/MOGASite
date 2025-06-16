using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers;
public class SeoController(ISeoService seoService) : ApiBaseController
{
    private readonly ISeoService _seoService = seoService;

    [HttpPost]
    public async Task<ActionResult<SeoResponse>> AddSeo([FromForm] AddSeoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var seo = await _seoService.AddSeoAsync(request, cancellationToken);
            return Ok(seo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("route/{route}")]
    public async Task<ActionResult<SeoResponse>> GetSeoByRoute(string route, CancellationToken cancellationToken)
    {
        try
        {
            var seo = await _seoService.GetSeoByRouteAsync(route, cancellationToken);
            return Ok(seo);
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SeoResponse>>> GetAllSeo(CancellationToken cancellationToken)
    {
        try
        {
            var seos = await _seoService.GetAllSeoAsync(cancellationToken);
            return Ok(seos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SeoResponse>> UpdateSeo(int id, [FromForm] AddSeoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var seo = await _seoService.UpdateSeoAsync(id, request, cancellationToken);
            return Ok(seo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeo(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _seoService.DeleteSeoAsync(id, cancellationToken);
            return Ok(new { Message = "SEO deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}