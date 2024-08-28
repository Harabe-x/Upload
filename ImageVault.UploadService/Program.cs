using ImageVault.UploadService.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.WebHost.UseUrls("http://*:2101");
builder.RegisterServices();
builder.RegisterDbContext();
builder.AddSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(); 
app.UseHttpsRedirection();
app.UseRabbitMqListener();
app.MapControllers();
app.Run();