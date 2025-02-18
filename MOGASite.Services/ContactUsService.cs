using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class ContactUsService(IUnitOfWork unitOfWork) : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ContactUsResponse> AddContactUsAsync(AddContactUsRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request cannot be empty.");
            }

            var contactUs = new ContactUs
            {
                FullName = request.FullName,
                Email = request.Email,
                Message = request.Message,
                FindWay = request.FindWay,
                Phone = request.Phone,

            };

            _unitOfWork.Repository<ContactUs>().Add(contactUs);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add contact us.");
            }

            return new ContactUsResponse
            {
                Id = contactUs.Id,
                FullName = contactUs.FullName,
                Email = contactUs.Email,
                Message = contactUs.Message,
                FindWay = contactUs.FindWay,
                Phone = contactUs.Phone,
                Date = contactUs.CreatedAt.ToShortDateString()
            };
        }

        public async Task<bool> DeleteContactUsAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var contactUs = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(id, cancellationToken);

            if (contactUs == null)
            {
                throw new ArgumentNullException("Contact Us not found.");
            }

            _unitOfWork.Repository<ContactUs>().Delete(contactUs);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if(result <= 0)
            {
                throw new Exception("Failed to delete contact us.");
            }

            return true;

        }

        public async Task<IReadOnlyList<ContactUsResponse>> GetContactUsAsync(CancellationToken cancellationToken = default)
        {
            var contactUs = await _unitOfWork.Repository<ContactUs>().GetAllAsync(cancellationToken);

            return contactUs
                .OrderByDescending(x => x.Id)
                .Select(x => new ContactUsResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Message = x.Message,
                    FindWay = x.FindWay,
                    Phone = x.Phone,
                    Date = x.CreatedAt.ToShortDateString()

                }).ToList().AsReadOnly();
        }
    }
}
