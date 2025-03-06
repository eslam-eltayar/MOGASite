using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MOGASite.Core.Entities;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Reposatories._Data;
using MOGASite.Reposatories.Repositories;
using MOGASite.Services;
using System.Reflection;

namespace MOGASite.APIs.Extensions
{
    public static class AddApplicationServicesExtenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });



            Services.AddFluentValidationAutoValidation();
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<IBlogService, BlogService>();
            Services.AddScoped<IFileUploadService, FileUploadService>();
            Services.AddScoped<IProjectService, ProjectService>();
            Services.AddScoped<IReviewService, ReviewService>();
            Services.AddScoped<ITeamService, TeamService>();
            Services.AddScoped<IContactUsService, ContactUsService>();
            Services.AddScoped<IQuotationService, QuotationService>();
            Services.AddScoped<IClientService, ClientService>();
            Services.AddScoped<IHostingService, HostingService>();
            Services.AddScoped<IServiceService, ServiceService>();

            Services.AddScoped<IMailService, MailService>();

            Services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            return Services;
        }
    }
}