using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MOGASite.Core.Repositories;
using MOGASite.Core.Services;
using MOGASite.Reposatories._Data;
using MOGASite.Reposatories.Repositories;
using MOGASite.Services;
using StackExchange.Redis;
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

            Services.AddRedisServices(configuration);

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
            Services.AddScoped<ISeoService, SeoService>();

            Services.AddScoped<IMailService, MailService>();

            Services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            return Services;
        }

        private static IServiceCollection AddRedisServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                var redisConfig = configuration.GetSection("Redis");

                var options = new ConfigurationOptions
                {
                    EndPoints = { { "redis-16641.c9.us-east-1-4.ec2.redns.redis-cloud.com", 16641 } },
                    User = "default",
                    Password = "nS68yUmVKa0F2M1MYbwUat3SWI1zo5KX"
                };

                return ConnectionMultiplexer.Connect(options);
            });

            return Services;
        }
    }
}