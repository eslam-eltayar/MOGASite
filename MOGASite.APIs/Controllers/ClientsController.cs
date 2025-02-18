using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Services;

namespace MOGASite.APIs.Controllers
{
    public class ClientsController(IClientService clientService) : ApiBaseController
    {
        private readonly IClientService _clientService = clientService;

        [HttpPost("")]
        public async Task<ActionResult<ClientResponse>> AddClient([FromForm] AddClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newClient = await _clientService.AddClientAsync(request, cancellationToken);
                return Ok(newClient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("")]
        public async Task<ActionResult<IReadOnlyList<ClientResponse>>> GetClients(CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _clientService.GetClientsAsync(cancellationToken);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientResponse>> UpdateClient(int id, [FromForm] AddClientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedClient = await _clientService.UpdateClientAsync(id, request, cancellationToken);
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _clientService.DeleteClientAsync(id, cancellationToken);
                return Ok(new { Message = $"Client with id {id} Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

    }
}
