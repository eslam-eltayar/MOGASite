using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface ITeamService
    {
        Task<TeamResponse> AddTeamAsync(AddTeamRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TeamResponse>> GetMembersAsync(CancellationToken cancellationToken = default);

        Task<TeamResponse> UpdateMemberAsync(int id, AddTeamRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken = default);

    }
}
