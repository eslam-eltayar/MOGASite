using Microsoft.EntityFrameworkCore;
using MOGASite.Core.DTOs.Requests;
using MOGASite.Core.DTOs.Responses;
using MOGASite.Core.Entities;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Services.SEO;

namespace MOGASite.Services;

public class SeoService : ISeoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUploadService _fileUploadService;

    public SeoService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService)
    {
        _unitOfWork = unitOfWork;
        _fileUploadService = fileUploadService;
    }

    public async Task<SeoResponse> AddSeoAsync(AddSeoRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var existingSeo = await _unitOfWork.Repository<Seo>()
            .FirstOrDefaultAsync(s => s.Route == request.Route);

        if (existingSeo != null)
            throw new Exception($"SEO metadata already exists for route: {request.Route}");

        var seo = new Seo
        {
            Title = request.Title,
            Description = request.Description,
            Keywords = request.Keywords,
            Route = request.Route,
            OgTitle = request.OgTitle,
            OgDescription = request.OgDescription
        };

        if (request.OgImage != null)
        {
            string imageUrl = await _fileUploadService.UploadFileAsync(request.OgImage, "seo");
            seo.OgImage = imageUrl;
        }

        _unitOfWork.Repository<Seo>().Add(seo);

        int result = await _unitOfWork.CompleteAsync(cancellationToken);
        if (result <= 0)
            throw new Exception("Failed to save SEO metadata");

        return MapToResponse(seo);
    }

    public async Task<bool> DeleteSeoAsync(int id, CancellationToken cancellationToken = default)
    {
        var seo = await _unitOfWork.Repository<Seo>().GetByIdAsync(id);
        if (seo == null)
            throw new Exception("SEO metadata not found");

        _unitOfWork.Repository<Seo>().Delete(seo);

        int result = await _unitOfWork.CompleteAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IReadOnlyList<SeoResponse>> GetAllSeoAsync(CancellationToken cancellationToken = default)
    {
        var seoList = await _unitOfWork.Repository<Seo>().GetAllAsync();

        if (!seoList.Any())
            throw new Exception("No SEO metadata found");

        return seoList.Select(MapToResponse).ToList().AsReadOnly();
    }

    public async Task<SeoResponse> GetSeoByRouteAsync(string route, CancellationToken cancellationToken = default)
    {
        var seo = await _unitOfWork.Repository<Seo>()
            .FirstOrDefaultAsync(s => s.Route == route);

        if (seo == null)
            throw new Exception($"SEO metadata not found for route: {route}");

        return MapToResponse(seo);
    }

    public async Task<SeoResponse> UpdateSeoAsync(int id, AddSeoRequest request, CancellationToken cancellationToken = default)
    {
        var seo = await _unitOfWork.Repository<Seo>().GetByIdAsync(id);
        if (seo == null)
            throw new Exception("SEO metadata not found");

        // Check if the new route already exists for another SEO entry
        if (request.Route != seo.Route)
        {
            var existingSeo = await _unitOfWork.Repository<Seo>()
                .FirstOrDefaultAsync(s => s.Route == request.Route && s.Id != id);

            if (existingSeo != null)
                throw new Exception($"SEO metadata already exists for route: {request.Route}");
        }

        seo.Title = request.Title;
        seo.Description = request.Description;
        seo.Keywords = request.Keywords;
        seo.Route = request.Route;
        seo.OgTitle = request.OgTitle;
        seo.OgDescription = request.OgDescription;

        if (request.OgImage != null)
        {
            string imageUrl = await _fileUploadService.UploadFileAsync(request.OgImage, "seo");
            seo.OgImage = imageUrl;
        }

        _unitOfWork.Repository<Seo>().Update(seo);

        int result = await _unitOfWork.CompleteAsync(cancellationToken);
        if (result <= 0)
            throw new Exception("Failed to update SEO metadata");

        return MapToResponse(seo);
    }

    private static SeoResponse MapToResponse(Seo seo)
    {
        return new SeoResponse
        {
            Id = seo.Id,
            Title = seo.Title,
            Description = seo.Description,
            Keywords = seo.Keywords,
            Route = seo.Route,
            OgTitle = seo.OgTitle,
            OgDescription = seo.OgDescription,
            OgImage = seo.OgImage
        };
    }
}