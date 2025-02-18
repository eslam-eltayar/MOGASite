using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IClientService
    {
        Task<ClientResponse> AddClientAsync(AddClientRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ClientResponse>> GetClientsAsync(CancellationToken cancellationToken = default);
        Task<ClientResponse> UpdateClientAsync(int id, AddClientRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteClientAsync(int id, CancellationToken cancellationToken = default);
    }
}
