using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.WebHost.UseUrls("http://*:2101");

//These extension methods are located in the "Configuration" folder
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddJwtAuthentication();
builder.AddRateLimiting();
builder.AddX509Certificate2();
builder.AddSwagger();   
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run(); 