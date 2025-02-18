using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MOGASite.Core.Services;
using MOGASite.Reposatories._Data;
using MOGASite.Services;
using System.Text;

namespace MOGASite.APIs.Extensions
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<ITokenService, TokenService>();

            Services.AddIdentity<IdentityUser, IdentityRole>()
                                          .AddEntityFrameworkStores<ApplicationDbContext>();

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)   // User Manager / SignIn Manager / RoleManager
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });
            return Services;
        }
    }
}
