using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Enums;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Core.Specifications.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class ProjectService(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment) : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ProjectResponse> AddProjectAsync(AddProjectRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. The Body cannot be null");
            }


            if (request.HeadImage == null || request.HeadImage.Length == 0)
            {
                throw new ArgumentException("Head Image is required.");
            }

            string headImageUrl = await _fileUploadService.UploadFileAsync(request.HeadImage, "projects");

            var project = new Project
            {
                NameAR = request.NameAR,
                NameEN = request.NameEN,
                DescriptionAR = request.DescriptionAR,
                DescriptionEN = request.DescriptionEN,
                HeadImageUrl = headImageUrl,

            };

            if (Enum.TryParse<Category>(request.Category, true, out var parsedCategory))
            {
                project.Category = parsedCategory;
            }
            else
            {
                throw new ArgumentException($"Invalid Category value : {request.Category}");
            }

            if (Enum.TryParse<ProjectType>(request.Type, true, out var parsedType))
            {
                project.Type = parsedType;
            }
            else
            {
                throw new ArgumentException($"Invalid Project Type  value : {request.Type}");
            }

            _unitOfWork.Repository<Project>().Add(project);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("An error occurred while saving the project");
            }

            // Save media files

            var mediaItems = new List<ProjectMedia>();
            var mediaUrls = new List<string>();

            if (request.MediaFiles.Any())
            {
                foreach (var file in request.MediaFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileUrl = await _fileUploadService.UploadFileAsync(file, "projects");

                        var media = new ProjectMedia
                        {
                            ProjectId = project.Id,
                            MediaUrl = fileUrl
                        };

                        mediaItems.Add(media);
                        mediaUrls.Add(fileUrl);
                    }
                }

                await _unitOfWork.Repository<ProjectMedia>().AddRange(mediaItems);
                int mediaSaveResult = await _unitOfWork.CompleteAsync();

                if (mediaSaveResult <= 0)
                    throw new Exception("An error occurred while saving media files.");
            }

            foreach (var step in request.ProjectSteps)
            {
                var projectStep = new ProjectSteps
                {
                    ProjectId = project.Id,
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,


                };

                _unitOfWork.Repository<ProjectSteps>().Add(projectStep);
            }

            int projectStepResult = await _unitOfWork.CompleteAsync(cancellationToken);

            if (projectStepResult <= 0)
                throw new Exception("There's an error while adding Project Steps!");


            return new ProjectResponse
            {
                TitleAR = project.NameAR,
                TitleEN = project.NameEN,
                DescriptionAR = project.DescriptionAR,
                DescriptionEN = project.DescriptionEN,
                Category = project.Category.ToString(),
                MediaUrls = mediaUrls,
                ProjectId = project.Id,
                ProjectSteps = request.ProjectSteps.Select(step => new ProjectStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,

                }).ToList()

            };
        }

        public async Task<bool> DeleteProjectAsync(int projectId, CancellationToken cancellationToken = default)
        {

            if (projectId <= 0)
            {
                throw new ArgumentException("Invalid Project Id");
            }

            var spec = new ProjectWithStepsAndMediaSpecification(projectId);

            var project = await _unitOfWork.Repository<Project>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (project is null)
            {
                throw new Exception("Project not found");
            }

            // Delete media files

            if (project.MediaItems != null && project.MediaItems.Any())
            {
                foreach (var media in project.MediaItems)
                {
                    if (!string.IsNullOrEmpty(media.MediaUrl))
                    {
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "projects", media.MediaUrl);

                        filePath = $"wwwroot{filePath}";

                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                }
            }

            project.ProjectSteps.Clear();

            _unitOfWork.Repository<Project>().Delete(project);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result <= 0)
            {
                throw new Exception("An error occurred while deleting the project");
            }
            return true;
        }

        public async Task<ProjectResponse> GetProjectAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var spec = new ProjectWithStepsAndMediaSpecification(projectId);

            var project = await _unitOfWork.Repository<Project>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (project is null)
            {
                throw new Exception("Project not found");
            }

            return new ProjectResponse
            {
                TitleAR = project.NameAR,
                TitleEN = project.NameEN,
                DescriptionAR = project.DescriptionAR,
                DescriptionEN = project.DescriptionEN,
                Category = project.Category.ToString(),
                MediaUrls = project.MediaItems.Select(media => media.MediaUrl).ToList(),
                ProjectId = project.Id,
                HeadImageUrl = project.HeadImageUrl,
                Type = project.Type.ToString(),

                ProjectSteps = project.ProjectSteps.Select(step => new ProjectStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                }).ToList()

            };

        }

        public async Task<IReadOnlyList<ProjectResponse>> GetProjectsAsync(CancellationToken cancellationToken = default)   
        {
            var spec = new ProjectWithStepsAndMediaSpecification();

            var projects = await _unitOfWork.Repository<Project>().GetAllWithSpecAsync(spec, cancellationToken);

            if (projects == null || !projects.Any())
            {
                throw new Exception("No projects found");
            }

            return projects.Select(project => new ProjectResponse
            {
                TitleAR = project.NameAR,
                TitleEN = project.NameEN,
                DescriptionAR = project.DescriptionAR,
                DescriptionEN = project.DescriptionEN,
                Category = project.Category.ToString(),
                MediaUrls = project.MediaItems.Select(media => media.MediaUrl).ToList(),
                ProjectId = project.Id,
                HeadImageUrl = project.HeadImageUrl,
                Type = project.Type.ToString(),

                ProjectSteps = project.ProjectSteps.Select(step => new ProjectStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,

                }).ToList()

            }).ToList().AsReadOnly();

        }

        public async Task<IReadOnlyList<ProjectResponse>> GetProjectsByCategoryAsync(ProjectByCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var spec = new ProjectWithStepsAndMediaSpecification(request.Category);

            var projects = await _unitOfWork.Repository<Project>().GetAllWithSpecAsync(spec);

            if (projects == null || !projects.Any())
                throw new Exception("Not Projects founded!");

            return projects.Select(project => new ProjectResponse
            {
                TitleAR = project.NameAR,
                TitleEN = project.NameEN,
                DescriptionAR = project.DescriptionAR,
                DescriptionEN = project.DescriptionEN,
                Category = project.Category.ToString(),
                MediaUrls = project.MediaItems.Select(media => media.MediaUrl).ToList(),
                ProjectId = project.Id,
                HeadImageUrl = project.HeadImageUrl,
                Type = project.Type.ToString(),

                ProjectSteps = project.ProjectSteps.Select(step => new ProjectStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,

                }).ToList()

            }).ToList().AsReadOnly();

        }

        public async Task<ProjectResponse> UpdateProjectAsync(int projectId, UpdateProjectRequest request, CancellationToken cancellationToken = default)
        {
            if (projectId <= 0)
            {
                throw new ArgumentException("Invalid Project Id");
            }

            var spec = new ProjectWithStepsAndMediaSpecification(projectId);

            var project = await _unitOfWork.Repository<Project>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (project is null)
            {
                throw new Exception("Project not found");
            }

            project.NameAR = request.NameAR;
            project.NameEN = request.NameEN;
            project.DescriptionAR = request.DescriptionAR;
            project.DescriptionEN = request.DescriptionEN;

            if (Enum.TryParse<Category>(request.Category, true, out var parsedCategory))
            {
                project.Category = parsedCategory;
            }
            else
            {
                throw new ArgumentException($"Invalid Category value : {request.Category}");
            }

            if (Enum.TryParse<ProjectType>(request.Type, true, out var parsedType))
            {
                project.Type = parsedType;
            }
            else
            {
                throw new ArgumentException($"Invalid Project Type  value : {request.Type}");
            }

            if (!string.IsNullOrEmpty(project.HeadImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "projects", project.HeadImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            var newHeadImageUrl = await _fileUploadService.UploadFileAsync(request.HeadImage, "projects");

            project.HeadImageUrl = newHeadImageUrl;

            var mediaItems = new List<ProjectMedia>();
            var mediaUrls = new List<string>();

            if (request.MediaFiles.Any())
            {
                project.MediaItems.Clear();

                foreach (var file in request.MediaFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileUrl = await _fileUploadService.UploadFileAsync(file, "projects");

                        var media = new ProjectMedia
                        {
                            ProjectId = project.Id,
                            MediaUrl = fileUrl
                        };

                        mediaItems.Add(media);
                        mediaUrls.Add(fileUrl);
                    }
                }

                await _unitOfWork.Repository<ProjectMedia>().AddRange(mediaItems);

            }

            if (request.ProjectSteps.Any())
            {
                project.ProjectSteps.Clear();

                foreach (var step in request.ProjectSteps)
                {
                    var projectStep = new ProjectSteps
                    {
                        ProjectId = project.Id,
                        TitleAR = step.TitleAR,
                        TitleEN = step.TitleEN,
                        DescriptionAR = step.DescriptionAR,
                        DescriptionEN = step.DescriptionEN,
                    };
                    _unitOfWork.Repository<ProjectSteps>().Add(projectStep);
                }
            }

            _unitOfWork.Repository<Project>().Update(project);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("An error occurred while updating the project");
            }

            return new ProjectResponse
            {
                TitleAR = project.NameAR,
                TitleEN = project.NameEN,
                DescriptionAR = project.DescriptionAR,
                DescriptionEN = project.DescriptionEN,
                Category = project.Category.ToString(),
                MediaUrls = mediaUrls,
                ProjectId = project.Id,
                Type = project.Type.ToString(),
                HeadImageUrl = project.HeadImageUrl,
                ProjectSteps = request.ProjectSteps.Select(step => new ProjectStepsResponse
                {
                    TitleAR = step.TitleAR,
                    TitleEN = step.TitleEN,
                    DescriptionAR = step.DescriptionAR,
                    DescriptionEN = step.DescriptionEN,
                }).ToList()
            };


        }
    }
}
