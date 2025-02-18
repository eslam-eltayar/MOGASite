using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IContactUsService
    {
        Task<ContactUsResponse> AddContactUsAsync(AddContactUsRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ContactUsResponse>> GetContactUsAsync(CancellationToken cancellationToken = default);
        Task<bool> DeleteContactUsAsync(int id, CancellationToken cancellationToken = default);
    }
}
