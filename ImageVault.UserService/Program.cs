using ImageVault.UserService.Extension;
using ImageVault.UserService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls("http://*:2104");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddSwagger();
builder.AddJwtAuthentication();
builder.AddX509Certificate2();

var app = builder.Build();

app.AddRabbitMqConsumer();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();