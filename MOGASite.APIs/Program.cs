using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MOGASite.APIs.Extensions;
using MOGASite.APIs.Helpers;
using MOGASite.Reposatories._Identity;
using Serilog;
using StackExchange.Redis;

namespace MOGASite.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();
            builder.Services.AddCors();

            //builder.Services.AddSingleton<IConnectionMultiplexer>((servicesProvider) =>
            //{
            //    var connection = builder.Configuration.GetConnectionString("Redis");
            //    return ConnectionMultiplexer.Connect(connection);
            //});

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Configuration.AddUserSecrets<Program>();

            var sLogger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
            builder.Logging.AddSerilog(sLogger);

            var app = builder.Build();

            #region Apply All Pending Migrations [Update Database] and Data Seeding

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await ApplicationIdentityDbContextSeed.SeedUserAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error Has Been occurred during Seeding Data.");
            }

            #endregion

            //if (app.Environment.IsDevelopment())
            //{
            //app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseCors(options =>
                        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                        );

            //var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

            //app.UseCors(options =>
            //    options.WithOrigins(allowedOrigins)
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            //);

            //app.UseDefaultFiles();

            app.UseStaticFiles();

            // Middleware to handle Angular routing

            app.Use(async (context, next) =>
            {
                if (!context.Request.Path.StartsWithSegments("/api") && // Allow API routes
                    !System.IO.Path.HasExtension(context.Request.Path.Value)) // Redirect non-file requests
                {
                    context.Request.Path = "/index.html";
                }
                await next();
            });

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
