using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class TeamsController(ITeamService teamService) : ApiBaseController
    {
        private readonly ITeamService _teamService = teamService;

        [HttpPost("Member")]
        public async Task<ActionResult<TeamResponse>> AddTeam([FromForm] AddTeamRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newTeam = await _teamService.AddTeamAsync(request, cancellationToken);
                return Ok(newTeam);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("Members")]
        public async Task<ActionResult<IReadOnlyList<TeamResponse>>> GetMembers(CancellationToken cancellationToken)
        {
            try
            {
                var members = await _teamService.GetMembersAsync(cancellationToken);
                return Ok(members);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("Member/{id}")]
        public async Task<ActionResult<TeamResponse>> UpdateMember(int id, [FromForm] AddTeamRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedMember = await _teamService.UpdateMemberAsync(id, request, cancellationToken);
                return Ok(updatedMember);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("Member/{id}")]
        public async Task<IActionResult> DeleteMember(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _teamService.DeleteMemberAsync(id, cancellationToken);
                return Ok(new { Message = "Member Deleted Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
