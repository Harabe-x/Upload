using ImageVault.RequestMetricsService.Configuration;
using ImageVault.RequestMetricsService.Extension;
using ImageVault.RequestMetricsService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls("http://*:2106");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddSwagger();
builder.AddJwtAuthentication();
builder.AddX509Certificate2();
builder.AddCors();

var app = builder.Build();

app.UseMiddleware<AnonymousRequestLoggingMiddleware>();
app.UseCors("AllowAll");
app.UseAuthentication();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.AddRabbitMqListener();
app.UseHttpsRedirection();
app.MapFallbackToController("CollectFallback", "RequestCollector");
app.Run();