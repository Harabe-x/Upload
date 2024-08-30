using ImageVault.RequestMetricsService.Configuration;
using ImageVault.RequestMetricsService.Extension;

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

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.AddRabbitMqListener();
app.UseHttpsRedirection();
app.Run();