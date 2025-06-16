using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services;
public interface ISeoService
{
    Task<SeoResponse> AddSeoAsync(AddSeoRequest request, CancellationToken cancellationToken = default);
    Task<SeoResponse> GetSeoByRouteAsync(string route, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SeoResponse>> GetAllSeoAsync(CancellationToken cancellationToken = default);
    Task<SeoResponse> UpdateSeoAsync(int id, AddSeoRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteSeoAsync(int id, CancellationToken cancellationToken = default);
}