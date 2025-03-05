using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.APIs.Helpers;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class BlogsController(IBlogService blogService) : ApiBaseController
    {
        private readonly IBlogService _blogService = blogService;


        [HttpPost("")]
        public async Task<IActionResult> AddBlog([FromForm] AddBlogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newBlog = await _blogService.AddBlogAsync(request, cancellationToken);

                return Ok(newBlog);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var blog = await _blogService.GetBlogByIdAsync(id, cancellationToken);
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("AllBlogs")]
        public async Task<ActionResult<Pagination<IReadOnlyList<BlogResponse>>>> GetAllBlogs([FromQuery] PaginationDto paginationDto, CancellationToken cancellationToken)
        {
            try
            {
                var blogs = await _blogService.GetAllBlogsAsync(paginationDto, cancellationToken);

                int count = await _blogService.GetBlogsCountAsync(cancellationToken);

                return Ok(new Pagination<BlogResponse>(paginationDto.PageIndex, paginationDto.PageSize, blogs, count));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromForm] UpdateBlogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedBlog = await _blogService.UpdateBlogAsync(id, request, cancellationToken);

                return Ok(updatedBlog);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id, CancellationToken cancellationToken)
        {
            try
            {
                var isDeleted = await _blogService.DeleteBlogAsync(id, cancellationToken);

                return Ok(new { Message = $"Blog with id {id} Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
