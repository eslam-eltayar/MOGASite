using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IBlogService
    {
        Task<BlogResponse> AddBlogAsync(AddBlogRequest request, CancellationToken cancellationToken = default);
        Task<BlogResponse> GetBlogByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<BlogResponse>> GetAllBlogsAsync(PaginationDto paginationDto, CancellationToken cancellationToken = default);
        Task<int> GetBlogsCountAsync(CancellationToken cancellationToken = default);

        Task<BlogResponse> UpdateBlogAsync(int id, UpdateBlogRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteBlogAsync(int id, CancellationToken cancellationToken = default);

        Task<BlogResponse> GetBlogBySlugAsync(string slug, CancellationToken cancellationToken = default);

    }
}
