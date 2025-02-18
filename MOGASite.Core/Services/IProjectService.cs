using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IProjectService
    {
        Task<ProjectResponse> AddProjectAsync(AddProjectRequest request, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(CancellationToken cancellationToken = default);

        Task<ProjectResponse> GetProjectAsync(int projectId, CancellationToken cancellationToken = default);

        Task<ProjectResponse> UpdateProjectAsync(int projectId, UpdateProjectRequest request, CancellationToken cancellationToken = default);

        Task<bool> DeleteProjectAsync(int projectId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProjectResponse>> GetProjectsByCategoryAsync(ProjectByCategoryRequest request, CancellationToken cancellationToken = default);
    }
}
