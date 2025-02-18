using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.Services
{
    public interface IServiceService
    {
        Task<ServiceResponse> CreateServiceAsync(AddServiceRequest request, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateServiceAsync(int id, AddServiceRequest request, CancellationToken cancellationToken = default);
        Task<ServiceResponse> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ServiceResponse>> GetAllServicesAsync(ServiceByCategoryRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteServiceAsync(int id, CancellationToken cancellationToken = default);
    }
}
