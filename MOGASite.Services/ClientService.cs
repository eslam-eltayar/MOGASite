using Microsoft.AspNetCore.Hosting;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class ClientService(IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment) : IClientService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ClientResponse> AddClientAsync(AddClientRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request cannot be null or empty.");
            }

            var client = new Client
            {
                NameAR = request.NameAR,
                NameEN = request.NameEN,

            };

            if (request.Logo == null || request.Logo.Length == 0)
            {
                throw new ArgumentException("Logo is required.");
            }

            string logoUrl = await _fileUploadService.UploadFileAsync(request.Logo, "clients");

            client.LogoUrl = logoUrl;

            _unitOfWork.Repository<Client>().Add(client);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add client.");
            }

            return new ClientResponse
            {
                Id = client.Id,
                NameAR = client.NameAR,
                NameEN = client.NameEN,
                LogoUrl = client.LogoUrl
            };
        }

        public async Task<bool> DeleteClientAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new Exception("Invalid Id.");

            var client = await _unitOfWork.Repository<Client>().GetByIdAsync(id);

            if (client == null)
                throw new Exception("Client not found.");

            if (!string.IsNullOrEmpty(client.LogoUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "clients", client.LogoUrl);
                imagePath = $"wwwroot{imagePath}";
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            _unitOfWork.Repository<Client>().Delete(client);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                throw new Exception("Failed to delete client.");

            return true;

        }

        public async Task<IReadOnlyList<ClientResponse>> GetClientsAsync(CancellationToken cancellationToken = default)
        {

            var clients = await _unitOfWork.Repository<Client>().GetAllAsync(cancellationToken);

            if (clients == null || !clients.Any())
                throw new Exception("No clients found.");

            return clients.Select(x => new ClientResponse
            {
                Id = x.Id,
                LogoUrl = x.LogoUrl,
                NameAR = x.NameAR,
                NameEN = x.NameEN

            }).ToList();
        }

        public async Task<ClientResponse> UpdateClientAsync(int id, AddClientRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new Exception("Invalid Id.");

            var client = await _unitOfWork.Repository<Client>().GetByIdAsync(id);

            if (client == null)
                throw new Exception("Client not found.");

            client.NameAR = request.NameAR;
            client.NameEN = request.NameEN;


            if (!string.IsNullOrEmpty(client.LogoUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "clients", client.LogoUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            var newLogo = await _fileUploadService.UploadFileAsync(request.Logo, "clients");

            client.LogoUrl = newLogo;

            _unitOfWork.Repository<Client>().Update(client);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                throw new Exception("Failed to update client.");

            return new ClientResponse
            {
                Id = client.Id,
                LogoUrl = client.LogoUrl,
                NameAR = client.NameAR,
                NameEN = client.NameEN
            };


        }
    }
}
