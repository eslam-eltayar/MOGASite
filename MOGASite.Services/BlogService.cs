using Microsoft.AspNetCore.Hosting;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Enums;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Core.Specifications.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class BlogService(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment) : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<BlogResponse> AddBlogAsync(AddBlogRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. Request cannot be empty or null");
            }

            if (request.Image == null || request.Image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            string imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "blogs");

            var blog = new Blog
            {
                TitleAR = request.TitleAR,
                TitleEN = request.TitleEN,
                DescriptionAR = request.DescriptionAR,
                DescriptionEN = request.DescriptionEN,
                ImageUrl = imageUrl,
                ContentAR = request.ContentAR,
                ContentEN = request.ContentEN,

            };

            if (Enum.TryParse<Category>(request.Category, true, out var parsedCategory))
            {
                blog.Category = parsedCategory;
            }
            else
            {
                throw new ArgumentException($"Invalid Category value : {request.Category}");
            }

            _unitOfWork.Repository<Blog>().Add(blog);


            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                throw new Exception("There's an error while adding Blog!");


            //foreach (var blogContent in request.BlogContents)
            //{
            //    var content = new BlogContent
            //    {
            //        TitleAR = blogContent.TitleAR,
            //        TitleEN = blogContent.TitleEN,
            //        DescriptionAR = blogContent.DescriptionAR,
            //        DescriptionEN = blogContent.DescriptionEN,
            //        BlogId = blog.Id
            //    };
            //    _unitOfWork.Repository<BlogContent>().Add(content);
            //}

            //int blogContentResult = await _unitOfWork.CompleteAsync(cancellationToken);

            //if (blogContentResult <= 0)
            //    throw new Exception("There's an error while adding BlogContent!");

            return new BlogResponse
            {
                BlogId = blog.Id,
                TitleAR = blog.TitleAR,
                TitleEN = blog.TitleEN,
                DescriptionAR = blog.DescriptionAR,
                DescriptionEN = blog.DescriptionEN,
                ImageUrl = blog.ImageUrl,
                Category = blog.Category.ToString(),
                Date = blog.Date.ToString(),
                ContentAR = blog.ContentAR,
                ContentEN = blog.ContentEN,
                //BlogContents =
                //    blog.BlogContents.Select(c => new BlogContentResponse
                //    {
                //        TitleAR = c.TitleAR,
                //        TitleEN = c.TitleEN,
                //        DescriptionAR = c.DescriptionAR,
                //        DescriptionEN = c.DescriptionEN
                //    }).ToList()

            };

        }

        public async Task<BlogResponse> GetBlogByIdAsync(int id, CancellationToken cancellationToken)
        {
            var spec = new BlogWithContentsSpecification(id);

            var blog = await _unitOfWork.Repository<Blog>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }

            return new BlogResponse
            {
                BlogId = blog.Id,
                TitleAR = blog.TitleAR,
                TitleEN = blog.TitleEN,
                DescriptionAR = blog.DescriptionAR,
                DescriptionEN = blog.DescriptionEN,
                ImageUrl = blog.ImageUrl ?? string.Empty,
                Category = blog.Category.ToString(),
                Date = blog.Date.ToString(),
                ContentAR = blog.ContentAR,
                ContentEN = blog.ContentEN,
                //BlogContents = blog.BlogContents.Select(c => new BlogContentResponse
                //{
                //    TitleAR = c.TitleAR,
                //    TitleEN = c.TitleEN,
                //    DescriptionAR = c.DescriptionAR,
                //    DescriptionEN = c.DescriptionEN

                //}).ToList()
            };
        }

        public async Task<IReadOnlyList<BlogResponse>> GetAllBlogsAsync(PaginationDto paginationDto, CancellationToken cancellationToken)
        {
            var spec = new BlogWithContentsSpecification(paginationDto);

            var blogs = await _unitOfWork.Repository<Blog>().GetAllWithSpecAsync(spec, cancellationToken);

            if (blogs is null || !blogs.Any())
            {
                throw new Exception("No Blogs found");
            }

            return blogs.Select(blog => new BlogResponse
            {
                BlogId = blog.Id,
                TitleAR = blog.TitleAR,
                TitleEN = blog.TitleEN,
                DescriptionAR = blog.DescriptionAR,
                DescriptionEN = blog.DescriptionEN,
                ImageUrl = blog.ImageUrl ?? string.Empty,
                Category = blog.Category.ToString(),
                Date = blog.Date.ToString(),

                ContentAR = blog.ContentAR,
                ContentEN = blog.ContentEN,

                //BlogContents = blog.BlogContents.Select(c => new BlogContentResponse
                //{
                //    TitleAR = c.TitleAR,
                //    TitleEN = c.TitleEN,
                //    DescriptionAR = c.DescriptionAR,
                //    DescriptionEN = c.DescriptionEN
                //}).ToList()

            }).ToList().AsReadOnly();
        }

        public async Task<BlogResponse> UpdateBlogAsync(int id, UpdateBlogRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Blog Id");
            }

            if (request == null)
            {
                throw new ArgumentNullException("Invalid Input. Request cannot be empty or null");
            }

            var spec = new BlogWithContentsSpecification(id);

            var blog = await _unitOfWork.Repository<Blog>().GetByIdWithSpecAsync(spec, cancellationToken);

            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }

            if (request.Image == null || request.Image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            blog.TitleAR = request.TitleAR;
            blog.TitleEN = request.TitleEN;

            blog.DescriptionAR = request.DescriptionAR;
            blog.DescriptionEN = request.DescriptionEN;

            if (Enum.TryParse<Category>(request.Category, true, out var parsedCategory))
            {
                blog.Category = parsedCategory;
            }
            else
            {
                throw new ArgumentException($"Invalid Category value : {request.Category}");
            }

            if (!string.IsNullOrEmpty(blog.ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "blogs", blog.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            var newImage = await _fileUploadService.UploadFileAsync(request.Image, "blogs");

            blog.ImageUrl = newImage;

            //if (request.BlogContents != null && request.BlogContents.Count > 0)
            //{

            //    blog.BlogContents.Clear();

            //    foreach (var content in request.BlogContents)
            //    {
            //        var blogContent = new BlogContent
            //        {
            //            TitleAR = content.TitleAR,
            //            TitleEN = content.TitleEN,
            //            DescriptionAR = content.DescriptionAR,
            //            DescriptionEN = content.DescriptionEN,
            //            BlogId = blog.Id
            //        };


            //        _unitOfWork.Repository<BlogContent>().Add(blogContent);
            //    }
            //}

            _unitOfWork.Repository<Blog>().Update(blog);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                throw new Exception("There's an error while updating Blog!");

            return new BlogResponse
            {
                //BlogContents = blog.BlogContents.Select(c => new BlogContentResponse
                //{
                //    TitleAR = c.TitleAR,
                //    TitleEN = c.TitleEN,
                //    DescriptionAR = c.DescriptionAR,
                //    DescriptionEN = c.DescriptionEN
                //}).ToList(),

                BlogId = blog.Id,
                TitleAR = blog.TitleAR,
                TitleEN = blog.TitleEN,
                DescriptionAR = blog.DescriptionAR,
                DescriptionEN = blog.DescriptionEN,
                ImageUrl = blog.ImageUrl,
                Category = blog.Category.ToString(),
                Date = blog.Date.ToString(),
                ContentAR = blog.ContentAR,
                ContentEN = blog.ContentEN

            };
        }

        public async Task<bool> DeleteBlogAsync(int id, CancellationToken cancellationToken = default)
        {

            if (id <= 0)
            {
                throw new ArgumentException("Invalid Blog Id");
            }

            var blog = await _unitOfWork.Repository<Blog>().GetByIdAsync(id, cancellationToken);

            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found");
            }

            if (!string.IsNullOrEmpty(blog.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "blogs", blog.ImageUrl);
                imagePath = $"wwwroot{imagePath}";
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            _unitOfWork.Repository<Blog>().Delete(blog);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
                throw new Exception("There's an error while deleting Blog!");

            return true;

        }

        public async Task<int> GetBlogsCountAsync(CancellationToken cancellationToken = default)
        {

            var spec = new BlogWithContentsSpecification();

            var count = await _unitOfWork.Repository<Blog>().GetCountAsync(spec);

            return count;

        }
    }
}
