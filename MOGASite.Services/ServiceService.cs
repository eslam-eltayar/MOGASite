using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Enums;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Core.Specifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class ServiceService(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment) : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ServiceResponse> CreateServiceAsync(AddServiceRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. The Body cannot be null");
            }

            if (request.Image == null || request.Image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            var service = new Service
            {
                TitleAR = request.TitleAR,
                TitleEN = request.TitleEN,
                DescriptionAR = request.DescriptionAR,
                DescriptionEN = request.DescriptionEN,
                BioAR = request.BioAR,
                BioEN = request.BioEN,
                

            };

            if (Enum.TryParse<ProjectType>(request.Type, true, out var parsedType))
            {
                service.Type = parsedType;
            }
            else
            {
                throw new ArgumentException($"Invalid Service Type  value : {request.Type}");
            }

            string imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "services");

            service.Image = imageUrl;

            _unitOfWork.Repository<Service>().Add(service);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("An error occurred while saving the service");
            }

            foreach (var step in request.ServiceSteps)
            {
                var serviceStep = new ServiceSteps
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                    BioAR = step.BioAR,
                    BioEN = step.BioEN,
                    ServiceId = service.Id,


                };

                if (step.Image != null && step.Image.Length > 0)
                {
                    string stepImageUrl = await _fileUploadService.UploadFileAsync(step.Image, "serviceSteps");
                    serviceStep.Image = stepImageUrl;
                }

                _unitOfWork.Repository<ServiceSteps>().Add(serviceStep);
            }

            int serviceStepsResult = await _unitOfWork.CompleteAsync(cancellationToken);

            if (serviceStepsResult <= 0)
                throw new Exception("There's an error while adding Service Steps!");


            return new ServiceResponse
            {
                Id = service.Id,
                TitleAR = service.TitleAR,
                TitleEN = service.TitleEN,
                DescriptionAR = service.DescriptionAR,
                DescriptionEN = service.DescriptionEN,
                BioAR = service.BioAR,
                BioEN = service.BioEN,
                Image = service.Image,
                Type = service.Type.ToString(),
                ServiceSteps = service.ServiceSteps.Select(step => new ServiceStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                    BioAR = step.BioAR,
                    BioEN = step.BioEN,
                    Image = step.Image

                }).ToList()
            };
        }

        public async Task<bool> DeleteServiceAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Service Id");
            }

            var spec = new ServiceWithStepsSpecification(id);

            var service = await _unitOfWork.Repository<Service>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (service == null)
            {
                throw new Exception("Service Not Found");
            }

            if (!string.IsNullOrEmpty(service.Image))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "services", service.Image);
                imagePath = $"wwwroot{imagePath}";
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            if (service.ServiceSteps.Any())
            {
                foreach (var step in service.ServiceSteps)
                {
                    if (!string.IsNullOrEmpty(step.Image))
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "serviceSteps", step.Image);
                        imagePath = $"wwwroot{imagePath}";
                        if (File.Exists(imagePath))
                            File.Delete(imagePath);
                    }
                }
            }

            _unitOfWork.Repository<Service>().Delete(service);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("An error occurred while deleting the service");
            }

            return true;

        }

        public async Task<IReadOnlyList<ServiceResponse>> GetAllServicesAsync(ServiceByCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var spec = new ServiceWithStepsSpecification(request);

            var services = await _unitOfWork.Repository<Service>().GetAllWithSpecAsync(spec, cancellationToken);

            if (services == null || !services.Any())
            {
                throw new Exception("No Services Found");
            }

            return services.Select(service => new ServiceResponse
            {
                Id = service.Id,
                TitleAR = service.TitleAR,
                TitleEN = service.TitleEN,
                DescriptionAR = service.DescriptionAR,
                DescriptionEN = service.DescriptionEN,
                BioAR = service.BioAR,
                BioEN = service.BioEN,
                Image = service.Image,
                Type = service.Type.ToString(),

                ServiceSteps = service.ServiceSteps.Select(step => new ServiceStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                    BioAR = step.BioAR,
                    BioEN = step.BioEN,
                    Image = step.Image
                }).ToList()

            }).ToList().AsReadOnly();

        }

        public async Task<ServiceResponse> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var spec = new ServiceWithStepsSpecification(id);

            var service = await _unitOfWork.Repository<Service>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (service == null)
            {
                throw new Exception("Service Not Found");
            }

            return new ServiceResponse
            {
                Id = service.Id,
                TitleAR = service.TitleAR,
                TitleEN = service.TitleEN,
                DescriptionAR = service.DescriptionAR,
                DescriptionEN = service.DescriptionEN,
                BioAR = service.BioAR,
                BioEN = service.BioEN,
                Image = service.Image,
                Type = service.Type.ToString(),
                ServiceSteps = service.ServiceSteps.Select(step => new ServiceStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                    BioAR = step.BioAR,
                    BioEN = step.BioEN,
                    Image = step.Image
                }).ToList()
            };
        }

        public async Task<ServiceResponse> UpdateServiceAsync(int id, AddServiceRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Service Id");
            }

            var spec = new ServiceWithStepsSpecification(id);

            var service = await _unitOfWork.Repository<Service>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (service == null)
            {
                throw new Exception("Service Not Found");
            }

            service.TitleAR = request.TitleAR;
            service.TitleEN = request.TitleEN;
            service.DescriptionAR = request.DescriptionAR;
            service.DescriptionEN = request.DescriptionEN;
            service.BioAR = request.BioAR;
            service.BioEN = request.BioEN;


            if (Enum.TryParse<ProjectType>(request.Type, true, out var parsedType))
            {
                service.Type = parsedType;
            }
            else
            {
                throw new ArgumentException($"Invalid Service Type  value : {request.Type}");
            }

            if (!string.IsNullOrEmpty(service.Image))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "services", service.Image);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            var newImage = await _fileUploadService.UploadFileAsync(request.Image, "services");

            service.Image = newImage;

            if (request.ServiceSteps != null && request.ServiceSteps.Count > 0)
            {

                foreach (var oldStep in service.ServiceSteps)
                {
                    if (!string.IsNullOrEmpty(oldStep.Image))
                    {
                        var stepImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "serviceSteps", oldStep.Image);
                        stepImagePath = $"wwwroot{stepImagePath}";

                        if (File.Exists(stepImagePath))
                            File.Delete(stepImagePath);
                    }
                }

                service.ServiceSteps.Clear();

                foreach (var step in request.ServiceSteps)
                {
                    var serviceStep = new ServiceSteps
                    {
                        TitleAR = step.TitleAR,
                        TitleEN = step.TitleEN,
                        DescriptionAR = step.DescriptionAR,
                        DescriptionEN = step.DescriptionEN,
                        BioAR = step.BioAR,
                        BioEN = step.BioEN,
                        ServiceId = service.Id
                    };

                    if (step.Image != null && step.Image.Length > 0)
                    {
                        string stepImageUrl = await _fileUploadService.UploadFileAsync(step.Image, "serviceSteps");
                        serviceStep.Image = stepImageUrl;
                    }

                    _unitOfWork.Repository<ServiceSteps>().Add(serviceStep);
                }
            }

            _unitOfWork.Repository<Service>().Update(service);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("An error occurred while updating the service");
            }

            return new ServiceResponse
            {
                Id = service.Id,
                TitleAR = service.TitleAR,
                TitleEN = service.TitleEN,
                DescriptionAR = service.DescriptionAR,
                DescriptionEN = service.DescriptionEN,
                BioAR = service.BioAR,
                BioEN = service.BioEN,
                Image = service.Image,
                Type = service.Type.ToString(),
                ServiceSteps = service.ServiceSteps.Select(step => new ServiceStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                    BioAR = step.BioAR,
                    BioEN = step.BioEN,
                    Image = step.Image
                }).ToList()
            };
        }
    }
}
