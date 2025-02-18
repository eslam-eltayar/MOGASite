using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Core.Specifications.Hostings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class HostingService(IUnitOfWork unitOfWork) : IHostingService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<HostingResponse> AddHostingAsync(AddHostingRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid input. The body cannot be empty.");
            }

            var hosting = new Hosting
            {
                NameAR = request.NameAR,
                NameEN = request.NameEN,
                Price = request.Price,
                IsBest = request.IsBest
            };

            _unitOfWork.Repository<Hosting>().Add(hosting);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add hosting.");
            }

            foreach (var prop in request.HostingProperties)
            {
                var hostingProp = new HostingProperties
                {
                    HostingId = hosting.Id,
                    TitleAR = prop.TitleAR,
                    TitleEN = prop.TitleEN

                };

                _unitOfWork.Repository<HostingProperties>().Add(hostingProp);
            }

            int propResult = await _unitOfWork.CompleteAsync(cancellationToken);

            if (propResult <= 0)
            {
                throw new Exception("Failed to add hosting properties.");
            }

            return new HostingResponse
            {
                Id = hosting.Id,
                NameAR = hosting.NameAR,
                NameEN = hosting.NameEN,
                Price = hosting.Price,
                IsBest = hosting.IsBest,
                Hosting_Properties = hosting.HostingProperties.Select(x => new HostingPropertiesRequest
                {
                    TitleAR = x.TitleAR,
                    TitleEN = x.TitleEN
                }).ToList()
            };
        }

        public async Task<bool> DeleteHostingAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var hosting = await _unitOfWork.Repository<Hosting>().GetByIdAsync(id, cancellationToken);

            if (hosting == null)
            {
                throw new Exception("Hosting not found.");
            }

            _unitOfWork.Repository<Hosting>().Delete(hosting);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);


            if (result <= 0)
            {
                throw new Exception("Failed to delete hosting.");
            }

            return true;
        }

        public async Task<IReadOnlyList<HostingResponse>> GetHostingAsync(CancellationToken cancellationToken = default)
        {

            var spec = new HostingWithPropertiesSpecification();

            var hostings = await _unitOfWork.Repository<Hosting>().GetAllWithSpecAsync(spec, cancellationToken);

            if (hostings == null || !hostings.Any())
            {
                throw new Exception("No hostings found.");
            }

            return hostings.Select(hosting => new HostingResponse
            {
                Id = hosting.Id,
                NameAR = hosting.NameAR,
                NameEN = hosting.NameEN,
                Price = hosting.Price,
                IsBest = hosting.IsBest,
                Hosting_Properties = hosting.HostingProperties.Select(x => new HostingPropertiesRequest
                {
                    TitleAR = x.TitleAR,
                    TitleEN = x.TitleEN
                }).ToList()


            }).ToList().AsReadOnly();
        }

        public async Task<HostingResponse> UpdateHostingAsync(int id, AddHostingRequest request, CancellationToken cancellationToken = default)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var hosting = await _unitOfWork.Repository<Hosting>().GetByIdAsync(id, cancellationToken);

            if (hosting == null)
            {
                throw new Exception("Hosting not found.");
            }

            hosting.NameAR = request.NameAR;
            hosting.NameEN = request.NameEN;
            hosting.Price = request.Price;
            hosting.IsBest = request.IsBest;

            if (request.HostingProperties.Any())
            {
                hosting.HostingProperties.Clear();

                foreach (var prop in request.HostingProperties)
                {
                    var hostingProp = new HostingProperties
                    {
                        HostingId = hosting.Id,
                        TitleAR = prop.TitleAR,
                        TitleEN = prop.TitleEN

                    };

                    _unitOfWork.Repository<HostingProperties>().Add(hostingProp);
                }
            }

            _unitOfWork.Repository<Hosting>().Update(hosting);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to update hosting.");
            }

            return new HostingResponse
            {
                Id = hosting.Id,
                NameAR = hosting.NameAR,
                NameEN = hosting.NameEN,
                Price = hosting.Price,
                IsBest = hosting.IsBest,
                Hosting_Properties = hosting.HostingProperties.Select(x => new HostingPropertiesRequest
                {
                    TitleAR = x.TitleAR,
                    TitleEN = x.TitleEN
                }).ToList()
            };

        }
    }
}
