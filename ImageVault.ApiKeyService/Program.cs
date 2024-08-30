using ImageVault.ApiKeyService.Configuration;
using ImageVault.ApiKeyService.Extension;
using ImageVault.ApiKeyService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls("http://*:2108");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddX509Certificate2();
builder.AddJwtAuthentication();
builder.AddSwagger();
builder.AddCors();
var app = builder.Build();


app.UseCors("ALlowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.AddRabbitMqListener();
app.MapControllers();
app.Run();