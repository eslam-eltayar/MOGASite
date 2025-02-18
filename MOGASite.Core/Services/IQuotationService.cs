using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IQuotationService
    {
        Task<QuotationResponse> AddQuotationAsync(AddQuotationRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<QuotationResponse>> GetQuotationsAsync(CancellationToken cancellationToken = default);
        Task<bool> DeleteQuotationAsync(int id, CancellationToken cancellationToken = default);
    }
}
