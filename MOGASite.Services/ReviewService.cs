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
    public class ReviewService(
        IUnitOfWork unitOfWork,
        IFileUploadService fileUploadService,
        IWebHostEnvironment webHostEnvironment) : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileUploadService _fileUploadService = fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ReviewResponse> AddReviewAsync(AddReviewRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Invalid input. The Request cannot be empty.");
            }

            var review = new Review
            {
                FirstNameEN = request.FirstNameEN,
                FirstNameAR = request.FirstNameAR,
                LastNameEN = request.LastNameEN,
                LastNameAR = request.LastNameAR,
                PositionEN = request.PositionEN,
                PositionAR = request.PositionAR,
                ReviewTextEN = request.ReviewTextEN,
                ReviewTextAR = request.ReviewTextAR,
                Stars = request.Stars,
            };

            if (request.Image != null && request.Image.Length > 0)
            {
                string imageUrl = await _fileUploadService.UploadFileAsync(request.Image, "reviews");

                review.ImageUrl = imageUrl;
            }

            _unitOfWork.Repository<Review>().Add(review);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to add review.");
            }

            return new ReviewResponse
            {
                Id = review.Id,
                FirstNameEN = review.FirstNameEN,
                FirstNameAR = review.FirstNameAR,
                LastNameEN = review.LastNameEN,
                LastNameAR = review.LastNameAR,
                PositionEN = review.PositionEN,
                PositionAR = review.PositionAR,
                ReviewTextEN = review.ReviewTextEN,
                ReviewTextAR = review.ReviewTextAR,
                Stars = review.Stars,
                ImageUrl = review.ImageUrl
            };

        }

        public async Task<bool> DeleteReviewAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(id, cancellationToken);

            if (review == null)
            {
                throw new Exception("Review not found.");
            }

            if (!string.IsNullOrEmpty(review.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "reviews", review.ImageUrl);

                imagePath = $"wwwroot{imagePath}";
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            _unitOfWork.Repository<Review>().Delete(review);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to delete review.");
            }

            return true;
        }

        public async Task<IReadOnlyList<ReviewResponse>> GetReviewsAsync(CancellationToken cancellationToken = default)
        {

            var reviews = await _unitOfWork.Repository<Review>().GetAllAsync(cancellationToken);

            if (reviews == null)
            {
                throw new Exception("No reviews found.");
            }

            return reviews.Select(review => new ReviewResponse
            {
                Id = review.Id,
                FirstNameEN = review.FirstNameEN,
                FirstNameAR = review.FirstNameAR,
                LastNameEN = review.LastNameEN,
                LastNameAR = review.LastNameAR,
                PositionEN = review.PositionEN,
                PositionAR = review.PositionAR,
                ReviewTextEN = review.ReviewTextEN,
                ReviewTextAR = review.ReviewTextAR,
                Stars = review.Stars,
                ImageUrl = review.ImageUrl

            }).ToList().AsReadOnly();

        }

        public async Task<ReviewResponse> UpdateReviewAsync(int id, AddReviewRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Id");
            }

            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(id, cancellationToken);

            if (review == null)
            {
                throw new Exception("Review not found.");
            }

            review.FirstNameEN = request.FirstNameEN;
            review.FirstNameAR = request.FirstNameAR;
            review.LastNameEN = request.LastNameEN;
            review.LastNameAR = request.LastNameAR;
            review.PositionEN = request.PositionEN;
            review.PositionAR = request.PositionAR;
            review.ReviewTextEN = request.ReviewTextEN;
            review.ReviewTextAR = request.ReviewTextAR;
            review.Stars = request.Stars;


            if (!string.IsNullOrEmpty(review.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "reviews", review.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            if (request.Image != null && request.Image.Length > 0)
            {

                var newImage = await _fileUploadService.UploadFileAsync(request.Image, "reviews");

                review.ImageUrl = newImage;
            }

            _unitOfWork.Repository<Review>().Update(review);

            int result = await _unitOfWork.CompleteAsync(cancellationToken);

            if (result <= 0)
            {
                throw new Exception("Failed to update review.");
            }

            return new ReviewResponse
            {
                Id = review.Id,
                FirstNameEN = review.FirstNameEN,
                FirstNameAR = review.FirstNameAR,
                LastNameEN = review.LastNameEN,
                LastNameAR = review.LastNameAR,
                PositionEN = review.PositionEN,
                PositionAR = review.PositionAR,
                ReviewTextEN = review.ReviewTextEN,
                ReviewTextAR = review.ReviewTextAR,
                Stars = review.Stars,
                ImageUrl = review.ImageUrl
            };
        }
    }
}