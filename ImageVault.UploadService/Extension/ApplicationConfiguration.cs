using System.Security.Cryptography.X509Certificates;
using System.Threading.RateLimiting;
using ImageVault.UploadService.AmazonS3;
using ImageVault.UploadService.Configuration;
using ImageVault.UploadService.Data;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.RabbitMq;
using ImageVault.UploadService.RabbitMq.Consumers;
using ImageVault.UploadService.Repository;
using ImageVault.UploadService.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ImageVault.UploadService.Extension;

public static class ApplicationConfiguration
{

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSingleton<IAmazonS3Connection, AmazonS3Connection>();
        builder.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
        builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        builder.Services.AddSingleton<IRabbitMqListener, RabbitMqListener>();
        builder.Services.AddSingleton<IRabbitMqConsumerList, RabbitMqConsumerList>();
        builder.Services.AddScoped<IImageUploadRepository, ImageUploadRepository>();
        builder.Services.AddScoped<IRabbitMqMessageSender, RabbitMqMessageSender>();
        builder.Services.AddScoped<IImageProcessingService, ImageProcessingService>();
        builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
        builder.Services.AddSingleton<ApiKeyConsumer>();
    }
    
    public static void RegisterDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(EnvironmentVariables.GetDatabaseConnectionString());
        }); 
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
                    new string[] {  }
                }
            });
        });

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
    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }
}

