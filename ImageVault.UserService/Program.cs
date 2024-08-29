using ImageVault.UserService.Configuration;
using ImageVault.UserService.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseUrls("http://*:2104");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddJwtAuthentication();
builder.AddX509Certificate2();
builder.AddSwagger();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.AddRabbitMqConsumer();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();