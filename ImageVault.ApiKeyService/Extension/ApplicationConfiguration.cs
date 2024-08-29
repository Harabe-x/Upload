using System.Security.Cryptography.X509Certificates;
using System.Text;
using ImageVault.ApiKeyService.Configuration;
using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.Data.Interfaces.Services;
using ImageVault.ApiKeyService.RabbitMq;
using ImageVault.ApiKeyService.RabbitMq.Consumers;
using ImageVault.ApiKeyService.Repository;
using ImageVault.ApiKeyService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ImageVault.ApiKeyService.Extension;

public static class ApplicationConfiguration
{

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var validationService = new DataValidationService();
        DataValidationRules.AddRules(validationService);

        builder.Services.AddSingleton<IDataValidationService>(validationService);
        builder.Services.AddSingleton<IRabbitMqListener, RabbitMqListener>(); 
        builder.Services.AddSingleton<IRabbitMqConsumerList, RabbitMqConsumerList>();
        builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        builder.Services.AddSingleton<ApiKeyUsageConsumer>();
        builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
        builder.Services.AddScoped<IRabbitMqMessageSender, RabbitMqMessageSender>();
        builder.Services.AddScoped<IAdminApiKeyRepository, AdminApiKeyRepository>();
    }

    public static void RegisterDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(EnvironmentVariables.GetDatabaseConnectionString());
        });
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "imagevault.tech",
                ValidAudience = "imagevault.tech",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.GetJwtSigningKey())),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true, 
            };
        });
        builder.Services.AddAuthorization();
    }

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
