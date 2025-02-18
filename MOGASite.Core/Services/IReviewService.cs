using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IReviewService
    {
        Task<ReviewResponse> AddReviewAsync(AddReviewRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReviewResponse>> GetReviewsAsync(CancellationToken cancellationToken = default);
        Task<ReviewResponse> UpdateReviewAsync(int id, AddReviewRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteReviewAsync(int id, CancellationToken cancellationToken = default);
    }
}
