using ChatApplication.Application.ExternalServiceInterfaces;
using ChatApplication.Application.InternalServiceInterfaces;
using ChatApplication.Application.InternalServices;
using ChatApplication.Domain.RepositoryInterfaces;
using ChatApplication.Infrastructure.ExternalServices;
using ChatApplication.Infrastructure.Persistence;
using ChatApplication.Infrastructure.Repositories;
using ChatApplication.WebAPI.Filters.ExceptionFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChatApplication.WebAPI.StartupExtensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()      // IMPORTANT for SignalR
                          .SetIsOriginAllowed(_ => true); // Allow all origins (for testing only)
                });
            });
            #endregion

            #region JWT Config
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JWT_Issuer"],
                        ValidAudience = configuration["JWT_Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT_Key"] ?? "JUST_EXAMPLE_KEY_FOR_JWT_AUTHENTICATION_WHEN_THE_REAL_KEY_NOT_FOUND")),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            string? accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken) &&
                context.HttpContext.Request.Path.StartsWithSegments("/chatHub"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            #region SignalR
            services.AddSignalR();
            #endregion

            #region Controllers
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
            });
            #endregion

            #region Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            #endregion

            #region DbContext
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Internal Services
            services.AddScoped<IAccountService, AccountService>();
            #endregion

            #region ExternalServices
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IChatService, ChatService>();
            #endregion

            #region Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            #endregion

            return services;
        }
    }
}
