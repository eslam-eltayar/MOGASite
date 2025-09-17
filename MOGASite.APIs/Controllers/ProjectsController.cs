using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class ProjectsController(IProjectService projectService) : ApiBaseController
    {
        private readonly IProjectService _projectService = projectService;

        [HttpPost("")]
        public async Task<IActionResult> AddProject([FromForm] AddProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newProject = await _projectService.AddProjectAsync(request, cancellationToken);

                return Ok(newProject);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //[Cached(600)]
        [HttpGet("")]
        public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
        {
            try
            {
                var projects = await _projectService.GetProjectsAsync(cancellationToken);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _projectService.GetProjectAsync(id, cancellationToken);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //[Cached(600)]
        [HttpGet("BySlug/{slug}")]
        public async Task<IActionResult> GetProjectBySlug(string slug, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _projectService.GetProjectBySlugAsync(slug, cancellationToken);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // [Cached(600)]
        [HttpGet("ByCategory")]
        public async Task<IActionResult> GetProjectsByCategory([FromQuery] ProjectByCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var projects = await _projectService.GetProjectsByCategoryAsync(request, cancellationToken);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromForm] UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(id, request, cancellationToken);
                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
