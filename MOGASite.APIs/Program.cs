
using Microsoft.AspNetCore.Identity;
using MOGASite.APIs.Extensions;
using MOGASite.Reposatories._Data;
using MOGASite.Reposatories._Identity;

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

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddIdentityServices(builder.Configuration);



            var app = builder.Build();

            #region Apply All Pending Migrations [Update Database] and Data Seeding

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<ApplicationDbContext>();


            // Ask CLR for Creatig Object from DbContext Explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                var _userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                await ApplicationIdentityDbContextSeed.SeedUserAsync(_userManager);
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex);
                logger.LogError(ex, "An Error Has Been occured during Seeding Data.");
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

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
