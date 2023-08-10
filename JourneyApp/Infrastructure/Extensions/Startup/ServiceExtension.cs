using JourneyApp.Service.Foundations.Journeys;
using JourneyApp.Journey.Loggings;
using JourneyApp.Journey.RoleManagement;
using JourneyApp.Journey.SignInManagement;
using JourneyApp.Journey.Storages;
using JourneyApp.Journey.UserManagement;

using Microsoft.AspNetCore.Identity;
//using JourneyApp.Filters.Exceptions;
using JourneyApp.Infrastructure.Providers.Tokens;
using JourneyApp.Models.Configurations.Tokens;
using JourneyApp.Models.Entities.Roles;
using JourneyApp.Models.Entities.Users;
using JourneyApp.Service.Foundations.Roles;
using JourneyApp.Service.Foundations.SignIn;
using JourneyApp.Service.Foundations.Users;
using JourneyApp.Service.Processings.Accounts;
using JourneyApp.Service.Processings.journeys;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using JourneyApp.Infrastructure.Filters.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JourneyApp.Service.Foundations.Journey;

namespace JourneyApp.Infrastructure.Extensions.Startup
{
    public static class ServiceExtension
    {

        public static IServiceCollection AddEntrance(this IServiceCollection services)
        {
            services.AddScoped<IStorageJourney, StoragJourney>();
            services.AddScoped<IUserManagementJourney, UserManagementJourney>();
            services.AddScoped<IRoleManagementJourney, RoleManagementJourney>();
            services.AddScoped<ISignInManagerJourney, SignInManagerJourney>();
            services.AddTransient<ILoggingJourney, LoggingJourney>();

            return services;
        }

        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtConfiguration = configuration
               .GetSection(nameof(JwtConfiguration))
               .Get<JwtConfiguration>();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<StoragJourney>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfiguration.Key)),

                    };
                });


            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "JourneyApp",
                    Version = "v1"
                };

                options.SwaggerDoc(
                    name: "v1",
                    info: openApiInfo);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJourneyProccessingService, JourneyProcessingService>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISIgnInService, SignInService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<IUSerService, UserService>();

            return services;
        }

        public static IServiceCollection AddControllerWithFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
            );

            return services;
        }
    }
}
