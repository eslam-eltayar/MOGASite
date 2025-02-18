using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IHostingService
    {
        Task<HostingResponse> AddHostingAsync(AddHostingRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<HostingResponse>> GetHostingAsync(CancellationToken cancellationToken = default);
        Task<HostingResponse> UpdateHostingAsync(int id, AddHostingRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteHostingAsync(int id, CancellationToken cancellationToken = default);

    }
}
