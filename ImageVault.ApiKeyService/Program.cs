using System.Security.Cryptography.X509Certificates;
using System.Text;
using ImageVault.ApiKeyService.Configuration;
using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.Data.Interfaces.Services;
using ImageVault.ApiKeyService.Extension;
using ImageVault.ApiKeyService.Middleware;
using ImageVault.ApiKeyService.RabbitMq;
using ImageVault.ApiKeyService.RabbitMq.Consumers;
using ImageVault.ApiKeyService.Repository;
using ImageVault.ApiKeyService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var validationService = new DataValidationService();

DataValidationRules.AddRules(validationService);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddSingleton<IDataValidationService>(validationService);
builder.Services.AddSingleton<IRabbitMqListener, RabbitMqListener>(); 
builder.Services.AddSingleton<IRabbitMqConsumerList, RabbitMqConsumerList>();
builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
builder.Services.AddSingleton<ApiKeyUsageConsumer>();
builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
builder.Services.AddScoped<IRabbitMqMessageSender, RabbitMqMessageSender>();
builder.Services.AddScoped<IAdminApiKeyRepository, AdminApiKeyRepository>();


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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
});
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


builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(config =>
    {
        config.ServerCertificate = new X509Certificate2("/https/aspnetapp.pfx", "TestPassword");
    });
});

builder.WebHost.UseUrls("http://*:2108");

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.AddRabbitMqListener();

app.MapControllers();


app.Run();