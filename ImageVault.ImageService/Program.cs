
using ImageVault.ImageService.Configuration;
using ImageVault.ImageService.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.WebHost.UseUrls("http://*:2111");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddSwagger();
builder.AddJwtAuthentication();
builder.AddCors();
var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseRabbitMqListener();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
 