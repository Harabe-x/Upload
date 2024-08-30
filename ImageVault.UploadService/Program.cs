using ImageVault.UploadService.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.WebHost.UseUrls("http://*:2110");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddSwagger();
builder.AddCors();
var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI(); 
app.UseHttpsRedirection();
app.UseRabbitMqListener();
app.MapControllers();
app.Run();