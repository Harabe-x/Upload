using System.Text;
using ImageVault.ImageService.Amazon;
using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Data.Interfaces.Amazon;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.RabbitMq;
using ImageVault.ImageService.RabbitMq.Consumers;
using ImageVault.ImageService.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ImageVault.ImageService.Extension;

public static class ApplicationConfiguration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<IImageManagerRepository, ImageManagerRepository>();
        builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>(); 
        builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        builder.Services.AddSingleton<IRabbitMqListener, RabbitMqListener>();
        builder.Services.AddSingleton<IRabbitMqConsumerList, RabbitMqConsumerList>();
        builder.Services.AddSingleton<IAmazonS3Connection, AmazonS3Connection>();
        builder.Services.AddSingleton<ImageConsumer>();
        builder.Services.AddSingleton<ApiKeyConsumer>(); 
        
    }

    public static void RegisterDbContext(this WebApplicationBuilder builder)
    {
 
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        }); 
        
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
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])) 
            };
        });

    }
    
}