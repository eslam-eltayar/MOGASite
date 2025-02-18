using Microsoft.AspNetCore.Hosting;
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
    public class TeamService(
                IUnitOfWork unitOfWork,
                IFileUploadService fileUploadService,
                IWebHostEnvironment webHostEnvironment) : ITeamService

    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<TeamResponse> AddTeamAsync(AddTeamRequest request, CancellationToken cancellationToken = default)
        {

            if (request is null)
            {
                throw new ArgumentNullException("Invalid Input. The request cannot be empty");
            }

            var member = new TeamMember
            {
                FirstNameAR = request.FirstNameAR,
                LastNameAR = request.LastNameAR,
                FirstNameEN = request.FirstNameEN,
                LastNameEN = request.LastNameEN,
                PositionAR = request.PositionAR,
                PositionEN = request.PositionEN

            };

            if (request.Image != null && request.Image.Length > 0)
            {
                string imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "team");
                member.ImageUrl = imageUrl;
            }

            _unitOfWork.Repository<TeamMember>().Add(member);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add team member.");
            }

            return new TeamResponse
            {
                Id = member.Id,
                FirstNameAR = member.FirstNameAR,
                LastNameAR = member.LastNameAR,
                FirstNameEN = member.FirstNameEN,
                LastNameEN = member.LastNameEN,
                PositionAR = member.PositionAR,
                PositionEN = member.PositionEN,
                ImageUrl = member.ImageUrl
            };
        }

        public async Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken = default)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var member = await _unitOfWork.Repository<TeamMember>().GetByIdAsync(id, cancellationToken);

            if (member is null)
            {
                throw new Exception("Member not found.");
            }

            if (!string.IsNullOrEmpty(member.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "team", member.ImageUrl);

                imagePath = $"wwwroot{imagePath}";
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            _unitOfWork.Repository<TeamMember>().Delete(member);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to delete team member.");
            }

            return true;
        }

        public async Task<IReadOnlyList<TeamResponse>> GetMembersAsync(CancellationToken cancellationToken = default)
        {
            var members = await _unitOfWork.Repository<TeamMember>().GetAllAsync();

            if (members is null || !members.Any())
                throw new Exception("No Members founded.");

            return members.Select(m => new TeamResponse
            {
                Id = m.Id,
                FirstNameAR = m.FirstNameAR,
                LastNameAR = m.LastNameAR,
                FirstNameEN = m.FirstNameEN,
                LastNameEN = m.LastNameEN,
                PositionAR = m.PositionAR,
                PositionEN = m.PositionEN,
                ImageUrl = m.ImageUrl

            }).ToList().AsReadOnly();
        }

        public async Task<TeamResponse> UpdateMemberAsync(int id, AddTeamRequest request, CancellationToken cancellationToken = default)
        {

            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var member = await _unitOfWork.Repository<TeamMember>().GetByIdAsync(id, cancellationToken);

            if (member is null)
            {
                throw new Exception("Member not found.");
            }

            member.FirstNameAR = request.FirstNameAR;
            member.LastNameAR = request.LastNameAR;
            member.FirstNameEN = request.FirstNameEN;
            member.LastNameEN = request.LastNameEN;
            member.PositionAR = request.PositionAR;
            member.PositionEN = request.PositionEN;


            if (!string.IsNullOrEmpty(member.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "team", member.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            if (request.Image != null && request.Image.Length > 0)
            {

                var newImage = await _fileUploadService.UploadFileAsync(request.Image, "team");

                member.ImageUrl = newImage;
            }


            _unitOfWork.Repository<TeamMember>().Update(member);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to update team member.");
            }

            return new TeamResponse
            {
                Id = member.Id,
                FirstNameAR = member.FirstNameAR,
                LastNameAR = member.LastNameAR,
                FirstNameEN = member.FirstNameEN,
                LastNameEN = member.LastNameEN,
                PositionAR = member.PositionAR,
                PositionEN = member.PositionEN,
                ImageUrl = member.ImageUrl
            };
        }
    }
}
