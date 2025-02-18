using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class ReviewsController(IReviewService reviewService) : ApiBaseController
    {
        private readonly IReviewService _reviewService = reviewService;

        [HttpPost("")]
        public async Task<ActionResult<ReviewResponse>> AddReview([FromForm] AddReviewRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newReview = await _reviewService.AddReviewAsync(request, cancellationToken);
                return Ok(newReview);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<ReviewResponse>>> GetReviews(CancellationToken cancellationToken)
        {
            try
            {
                var reviews = await _reviewService.GetReviewsAsync(cancellationToken);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewResponse>> UpdateReview(int id, [FromForm] AddReviewRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedReview = await _reviewService.UpdateReviewAsync(id, request, cancellationToken);
                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(id, cancellationToken);
                return Ok(new { Message = "Review Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
