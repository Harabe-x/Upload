using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.RateLimiting;
using ImageVault.AuthenticationService.Data;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using ImageVault.AuthenticationService.Data.Interfaces.Services;
using ImageVault.AuthenticationService.Data.Models;
using ImageVault.AuthenticationService.RabbitMq;
using ImageVault.AuthenticationService.Repository;
using ImageVault.AuthenticationService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ImageVault.AuthenticationService.Configuration;


/// <summary>
///  
/// </summary>
public  static class ApplicationConfiguration
{
    /// <summary>
    ///  Registers services used by application
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IRabbitMqMessageSender, RabbitMqMessageSender>();
        builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        builder.Services.AddHostedService<ServicesTokenProvider>();
        var validator = new DataValidationService();
        DataValidationRules.AddRules(validator);
        builder.Services.AddSingleton<IDataValidationService>(validator);
    }
    
    /// <summary>
    ///  Adds a swagger and configures it so that authorization using the token's JWT is possible
    /// </summary>
    /// <param name="builder"></param>
    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UsersAPI", Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] { }
                }
            });
        });

    }

    /// <summary>
    ///  Configures authentication using a JWT token
    /// </summary>
    /// <param name="builder"></param>
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                    options.DefaultForbidScheme =
                        options.DefaultSignInScheme =
                            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(EnvironmentVariables.GetJwtSigningKey())) 
            };
        });
    }

    /// <summary>
    ///  Registers the application's DbContext and configures the IdenityFramework
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(EnvironmentVariables.GetDatabaseConnectionString());
        });


        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.,_@+";
        }).AddEntityFrameworkStores<ApplicationDbContext>();

    }

    /// <summary>
    ///  Add rate limiting and configure the limiting policy
    /// </summary>
    /// <param name="builder"></param>
    public static void AddRateLimiting(this WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(builder => builder.AddFixedWindowLimiter("login", options =>
        {
            options.Window = TimeSpan.FromMinutes(1);
            options.PermitLimit = 10;
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            options.QueueLimit = 0;
        }).AddFixedWindowLimiter("register", options =>
        {
            options.Window = TimeSpan.FromMinutes(5);
            options.PermitLimit = 1;
            options.QueueLimit = 0;
        }));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("https://localhost:5174")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    /// <summary>
    ///  Adds the X509Certificate2 certificate
    /// </summary>
    /// <param name="builder"></param>
    public static void AddX509Certificate2(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ConfigureHttpsDefaults(config =>
            {
                config.ServerCertificate = new X509Certificate2("/https/aspnetapp.pfx", "TestPassword");
            });
        });
    }
}