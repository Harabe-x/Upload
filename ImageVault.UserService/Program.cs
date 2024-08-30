using ImageVault.UserService.Configuration;
using ImageVault.UserService.Extension;
using ImageVault.UserService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls("http://*:2104");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddJwtAuthentication();
builder.AddX509Certificate2();
builder.AddCors();
builder.AddSwagger();


var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.AddRabbitMqConsumer();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();