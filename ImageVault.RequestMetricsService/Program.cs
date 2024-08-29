using System.Security.Cryptography.X509Certificates;
using System.Text;
using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;
using ImageVault.RequestMetricsService.Extension;
using ImageVault.RequestMetricsService.RabbitMq;
using ImageVault.RequestMetricsService.RabbitMq.Consumers;
using ImageVault.RequestMetricsService.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserRequestMetricsRepository, UserRequestMetricsRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddSingleton<IRabbitMqConsumerList, RabbitMqConsumerList>();
builder.Services.AddSingleton<IRabbitMqListener, RabbitMqListener>();
builder.Services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
builder.Services.AddSingleton<RequestInfoConsumer>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "imagevault.tech",
        ValidAudience = "imagevault.tech",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EnvironmentVariabldsadasdsadasdasdsdasasdasdasdasdasdasdaes.GetJwtSigningKey()")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true, 
    };
});
builder.Services.AddAuthorization();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(config =>
    {
        config.ServerCertificate = new X509Certificate2("/https/aspnetapp.pfx", "TestPassword");
    });
});

builder.WebHost.UseUrls("http://*:2106");


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

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
app.AddRabbitMqListener();
app.UseHttpsRedirection();

app.Run();